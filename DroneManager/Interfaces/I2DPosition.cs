using DroneManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Interfaces
{
    public interface I2DPosition
    {
        double  Angle { get;  }
        Vector2dInt Position { get;  }

        string Position_String();
        string Position_Representation_String();
    }
}
