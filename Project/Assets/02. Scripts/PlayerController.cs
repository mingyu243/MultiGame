using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Networked] public NetworkObject ControlledCharacter { get; set; }
}
