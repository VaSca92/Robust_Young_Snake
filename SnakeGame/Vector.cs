using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame;
public class Vector2D
{
    public int x, y;
    public Vector2D(int x, int y) { this.x = x; this.y = y; }
    public static Vector2D operator +(Vector2D v1, Vector2D v2) { return new Vector2D(v1.x + v2.x, v1.y + v2.y); }
    public static Vector2D operator %(Vector2D v1, Vector2D v2) { return new Vector2D(v1.x % v2.x, v1.y % v2.y); }
}