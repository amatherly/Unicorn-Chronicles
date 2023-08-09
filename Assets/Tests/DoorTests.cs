using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTests
{

    private Door myTestDoor;

    [SetUp]
    public void SetUp()
    {
        myTestDoor = new GameObject().AddComponent<Door>();
        myTestDoor.MyOpenState = false;
        myTestDoor.MyLockState = false;
        myTestDoor.MyPlayer = new GameObject();
    }

    [Test]
    public void TestOpen()
    {
        myTestDoor.Open();
        Assert.True(myTestDoor.MyOpenState);
    }

    [Test]
    public void TestSetProximityTrigger()
    {
        myTestDoor.MyProximityTrigger = true;
        Assert.True(myTestDoor.MyProximityTrigger);
    }

    [Test]
    public void TestSetOpenState()
    {
        myTestDoor.MyOpenState = true;
        Assert.True(myTestDoor.MyOpenState);
    }

    [Test]
    public void TestSetLockState()
    {
        myTestDoor.MyLockState = true;
        Assert.True(myTestDoor.MyLockState);
    }

    [Test]
    public void SetHasAttempted()
    {
        myTestDoor.MyHasAttempted = true;
        Assert.True(myTestDoor.MyHasAttempted);
    }
}
