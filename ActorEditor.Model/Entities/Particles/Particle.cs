using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ActorEditor.Model.Entities.Particles
{
    class Particle
    {
        string _texturePath;
        HashSet<Vector3> _force;
        Constants constants;
        Copies _copies;
        Uniforms _uniforms;
        Expressions expressions;
        bool HasRelativeVelocity;
        bool StartsFull;
        
    }
}
