using NUnit.Framework;
using UnityEngine;

/// <summary>
/// NUnit tests for the <c>Door</c> script.
/// </summary>
public class DoorTests
{

    /// <summary>
    /// <c>Door</c> script for use in the tests.
    /// </summary>
    private Door myTestDoor;

    /// <summary>
    /// Called before every test to re-initialize the <c>myTestDoor</c> field
    /// and ensure valid state.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        myTestDoor = new GameObject().AddComponent<Door>();
        myTestDoor.MyOpenState = false;
        myTestDoor.MyLockState = false;
        myTestDoor.MyPlayer = new GameObject();
    }

    /// <summary>
    /// Test for the <c>Open()</c> method.
    /// </summary>
    [Test]
    public void TestOpen()
    {
        myTestDoor.Open();
        Assert.True(myTestDoor.MyOpenState);
    }

    /// <summary>
    /// Test for the <c>myProximityTrigger</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetProximityTrigger()
    {
        myTestDoor.MyProximityTrigger = true;
        Assert.True(myTestDoor.MyProximityTrigger);
    }

    /// <summary>
    /// Test for the <c>myOpenState</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetOpenState()
    {
        myTestDoor.MyOpenState = true;
        Assert.True(myTestDoor.MyOpenState);
    }

    /// <summary>
    /// Test for the <c>myLockState</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetLockState()
    {
        myTestDoor.MyLockState = true;
        Assert.True(myTestDoor.MyLockState);
    }

    /// <summary>
    /// Test for the <c>myHasAttempted</c> mutator method.
    /// </summary>
    [Test]
    public void SetHasAttempted()
    {
        myTestDoor.MyHasAttempted = true;
        Assert.True(myTestDoor.MyHasAttempted);
    }
}
