﻿using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class Mainboard : Component
{
    public int IdComponent { get; set; }

    public string? SerialNumber { get; set; }

    public virtual Component IdComponentNavigation { get; set; } = null!;
}