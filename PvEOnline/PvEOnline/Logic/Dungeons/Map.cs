using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DataTypes;
using PvEOnline.Dependencies;
using System.Collections;
using PvEOnline.Logic.Units;

namespace PvEOnline.Logic.Dungeons
{
    public class Map
    {
        string name;
        MapData info;
        Texture2D tileset;
        List<Rectangle> tiles;
        byte[,] background;
        byte[,] foreground;
        Tiles[,] properties;
        public Map(string name)
        {
            this.name = name;
        }
        public void LoadContent(ContentManager cont)
        {
            info = cont.Load<MapData>(@"Dungeons/" + name);
            tileset = cont.Load<Texture2D>(@"Dungeons/" + info.tileset);
            tiles = new List<Rectangle>();
            for (int x = 0; x < (int)info.tilesetSize.X; x++)
                for (int y = 0; y < (int)info.tilesetSize.Y; y++)
                    tiles.Add(new Rectangle(x * CONST.TILESIZEX, y * CONST.TILESIZEY, CONST.TILESIZEX, CONST.TILESIZEY));
            background = SDtoMD(info.background);
            foreground = SDtoMD(info.foreground);
            properties = new Tiles[CONST.MAPSIZEX, CONST.MAPSIZEY];
            //Properties are based on tiles
            Tiles[] tileList = (Tiles[])Enum.GetValues(typeof(Tiles));
            for (int x = 0; x < CONST.MAPSIZEX; x++)
                for (int y = 0; y < CONST.MAPSIZEY; y++)
                    properties[x, y] = tileList[info.properties[x * y]];
        }
        private byte[,] SDtoMD(byte[] sd)
        {
            byte[,] md = new byte[CONST.MAPSIZEX,CONST.MAPSIZEY];
            for (int x = 0; x < CONST.MAPSIZEX; x++)
                for (int y = 0; y < CONST.MAPSIZEY; y++)
                    md[x, y] = sd[x * y];
            return md;
        }
        public void Update(GameTime gameTime)
        {
            //nothing yet
        }

        public void DrawBackground(SpriteBatch sp)
        {
            for (int x = 0; x < CONST.MAPSIZEX; x++)
                for (int y = 0; y < CONST.MAPSIZEY; y++)
                    sp.Draw(tileset, new Vector2(x * CONST.TILESIZEX, y * CONST.TILESIZEY), tiles[background[x, y]], Color.White);
        }

        //-------------Below here pathfinding-------------------------- DO not look
        //Bresenham or something aixo
        public bool getLos(Point p1, Point p2, Tprop TPropFlags)
        {
            int x0 = p1.X, x1 = p2.X;
            int y0 = p1.Y, y1 = p2.Y;
            int dx = Math.Abs(x1 - x0), dy = Math.Abs(y1 - y0);
            int err = dx - dy;
            int sx = (x0 < x1) ? 1 : -1, sy = (y0 < y1) ? 1 : -1;
            while (x0!=x1||y0!=y1)
            {
                if (checkFlag(x0, y0,Tprop.Untargeteable))
                    return false;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
            return true;
        }
        private Point getLastLos(Point p1, Point p2,Tprop TPropFlags)
        {
            Point temp = p1;
            foreach (Point p in raycast(p1,p2)){
                if(checkFlag(p,TPropFlags))
                    return temp;
                temp = p;
            }
            return p2;
        }
        private Vector2 getLastLos(Vector2 vec1, Vector2 vec2, Tprop TPropFlags)
        {
            Point p1 = toMapIndices(vec1);
            Point p2 = toMapIndices(vec2);
            return toMapCoords(getLastLos(p1,p2,TPropFlags));
        }
        private void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        public Point toMapIndices(Vector2 vec)
        {
            return new Point((int)Math.Round(vec.X/ CONST.TILESIZEX), (int)Math.Round(vec.Y/ CONST.TILESIZEY));
        }
        private Vector2 toMapCoords(Point p)
        {
            return new Vector2(p.X * CONST.TILESIZEX , p.Y * CONST.TILESIZEY);
        }
        public List<Vector2> getRoute(Vector2 vec1, Vector2 vec2){
            Point p1 = toMapIndices(vec1);
            Point p2 = toMapIndices(vec2);
           
            return translateRoute(ThetaStar(p1, p2));
        }
        private List<Vector2> translateRoute(List<Point> route)
        {
            List<Vector2> res = new List<Vector2>();
            foreach (Point p in route)
                res.Add(toMapCoords(p));
            return res;
        }
        //Theta*
        private List<Point> ThetaStar(Point p1, Point p2)
        {
            HeapPriorityQueue<node> open = new HeapPriorityQueue<node>(CONST.MAPSIZEX * CONST.MAPSIZEY);
            List<Point> visited = new List<Point>(); ;
            node start = new node(null, p1,0);
            node last= new node(null,p2,0);
            float bestval=float.MaxValue;
            open.Enqueue(start,manhattan(p1,p2));
            while (open.Count > 0)
            {
                node curr = open.Dequeue();
                visited.Add(curr.pos);
                if (curr.isGoal(p2))
                    return curr.getRoute();
                foreach (node succ in getSuccesors(curr))
                {
                    if (!visited.Contains(succ.pos))
                    {
                        if (inOpen(open, succ.pos)==null)
                        {
                            succ._g = int.MaxValue;
                            succ.parent = null;
                        }
                        float oldg = succ._g;
                        calcCost(curr, succ);
                        if (succ._g < oldg) //TODO optimize
                        {
                            float hCost = manhattan(succ.pos, p2);
                            node found = inOpen(open, succ.pos);
                            if (found!=null)
                                open.UpdatePriority(found, succ._g + hCost);
                            else
                                open.Enqueue(succ, succ._g + hCost);
                                if ( hCost < bestval)
                            {
                                bestval =hCost;
                                last = (found!=null) ? found : succ;
                            }else if(hCost==bestval){
                                node temp = (found!=null) ? found : succ;
                                if (last._g > temp._g)
                                    last = temp;
                            }
                        }
                    }
                }
            }
            return last.getRoute() ;
        }
        private void calcCost(node curr, node succ)
        {
            node temp = (curr.parent != null && getLos(curr.parent.pos, succ.pos, Tprop.Untargeteable)) ? curr.parent : curr;
            float hCost = manhattan(temp.pos, succ.pos);
            if (temp._g + hCost < succ._g)
            {
                succ.parent = temp;
                succ._g = temp._g + hCost;
            }
        }
        private node inOpen(HeapPriorityQueue<node> open,Point  pos)
        {
            foreach (node n in open)
                if (n.pos == pos)
                    return n;
            return null;
        }
        private List<node> getSuccesors(node n){
            List<node> succ = new List<node>();
            int x = n.pos.X, y = n.pos.Y;
            calcSuccesor(succ,x+1, y, n);
            calcSuccesor(succ,x, y+1, n);
            calcSuccesor(succ,x-1, y, n);
            calcSuccesor(succ,x, y-1, n);
            return succ;
        }
        private void calcSuccesor(List<node> succ,int x,int y,node parent)
        {
            Point pos = new Point(x, y);
            if (x >= 0 && x < CONST.MAPSIZEX &&
                y >= 0 && y < CONST.MAPSIZEY &&
                !checkFlag(x, y, Tprop.Untargeteable) &&
                parent.pos != pos)
                succ.Add(new node(parent, pos, parent._g + 1));
        }
        private bool checkFlag(int x, int y,Tprop TPropFlags){
            return ((Tprop)(int)properties[x,y]).HasFlag(TPropFlags);
        }
        private bool checkFlag(Point p, Tprop TPropFlags)
        {
            return checkFlag(p.X, p.Y, TPropFlags);
        }
        //Manhattan distance
        private float manhattan(Point p1,Point p2){
            return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }
        // Bresenham
        private List<Point> raycast(Point p1, Point p2)
        {
            List<Point> res = new List<Point>();
            bool steep = Math.Abs(p2.Y - p1.Y) > Math.Abs(p2.X - p1.X);
            if (steep) { Swap<int>(ref p1.X, ref p1.Y); Swap<int>(ref p2.X, ref p2.Y); }
            if (p1.X > p2.X) { Swap<int>(ref p1.X, ref p2.X); Swap<int>(ref p1.Y, ref p2.Y); }
            int dX = (p2.X - p1.X), dY = Math.Abs(p2.Y - p1.Y), err = (dX / 2), ystep = (p1.Y < p2.Y ? 1 : -1), y = p1.Y;

            for (int x = p1.X; x <= p2.X; ++x)
            {
                res.Add((steep ? new Point(y, x) : new Point(x, y)));
                err = err - dY;
                if (err < 0) { y += ystep; err += dX; }
            }
            return res;
        }
    }
    public enum Tiles
    {
        Floor = Tprop.None,
        Wall = Tprop.Impassable | Tprop.Untargeteable,
        Hole = Tprop.Untargeteable,

    }
    [Flags]
    public enum Tprop
    {
        None = 0,
        Impassable = 1<<0,
        Untargeteable = 1<<1,
    }
    class node : PriorityQueueNode
    {
        public Point pos;
        public node parent;
        public float _g;

        public node(node parent,Point pos,float gcost){
            this.parent=parent;
            this.pos=pos;
            this._g = gcost;
        }
        public bool isGoal(Point goal){
            return pos ==goal;
        }
        public List<Point> getRoute(){
            List<Point> result = new List<Point>();
            node curr = this;
            while (curr != null)
            {
                result.Add(curr.pos);
                curr = curr.parent;
            }
            return result;
        }
    }
}
