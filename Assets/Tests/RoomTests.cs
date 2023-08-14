using NUnit.Framework;
using System.Collections.Generic;
using Common.Scripts.Maze;
using UnityEngine;

/// <summary>
/// NUnit tests for the <c>Room</c> script.
/// </summary>
public class RoomTests
{

    /// <summary>
    /// <c>Room</c> script for use in the tests.
    /// </summary>
    private Room myTestRoom;

    /// <summary>
    /// Called before every test to re-initialize the <c>myTestRoom</c> field and
    /// ensure valid state.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        myTestRoom = new GameObject().AddComponent<Room>();
        myTestRoom.MyRow = 1;
        myTestRoom.MyCol = 1;
    }

    /// <summary>
    /// Test for the <c>myRow</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetRow()
    {
        myTestRoom.MyRow = 2;
        Assert.AreEqual(myTestRoom.MyRow, 2);
    }

    /// <summary>
    /// Test for the <c>myCol</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetCol()
    {
        myTestRoom.MyCol = 2;
        Assert.AreEqual(myTestRoom.MyCol, 2);
    }

    /// <summary>
    /// Test for the <c>myHasVisited</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetHasVisited()
    {
        myTestRoom.MyHasVisited = true;
        Assert.True(myTestRoom.MyHasVisited);
    }

    /// <summary>
    /// Test for the <c>myDoors</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetDoors()
    {
        List<GameObject> testDoors = new List<GameObject>();
        myTestRoom.MyDoors = testDoors;
        Assert.AreEqual(testDoors, myTestRoom.MyDoors);
    }

    /// <summary>
    /// Test for the <c>Equals()</c> method when the <c>Room</c> scripts
    /// are not equal.
    /// </summary>
    [Test]
    public void TestNotEqual()
    {
        Room other = new GameObject().AddComponent<Room>();
        other.MyRow = 1;
        other.MyCol = 2;
        Assert.False(myTestRoom.Equals(other));
    }

    /// <summary>
    /// Test for the <c>Equals()</c> method when the <c>Room</c> scripts
    /// are equal.
    /// </summary>
    [Test]
    public void TestEquals()
    {
        Room other = new GameObject().AddComponent<Room>();
        other.MyRow = 1;
        other.MyCol = 1;
        Assert.True(myTestRoom.Equals(other));
    }
}
