using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6Explosions
{
    public enum ParticleType { Ship, Spark }

    public class Particle
    {
        public Rectangle DrawRectangle { get; set; }
        public Rectangle? SourceRectangle { get; set; }
        public float Rotation { get; set; }
        public float RandomRotation { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Origin { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public ParticleType ParticleType { get; set; }
    }
}
