using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTests
{

    private Room myTestRoom;

    [SetUp]
    public void SetUp()
    {
        myTestRoom = new GameObject().AddComponent<Room>();
        myTestRoom.MyRow = 1;
        myTestRoom.MyCol = 1;
    }

    [Test]
    public void TestSetRow()
    {
        myTestRoom.MyRow = 2;
        Assert.AreEqual(myTestRoom.MyRow, 2);
    }

    [Test]
    public void TestSetCol()
    {
        myTestRoom.MyCol = 2;
        Assert.AreEqual(myTestRoom.MyCol, 2);
    }

    [Test]
    public void TestSetHasVisited()
    {
        myTestRoom.MyHasVisited = true;
        Assert.True(myTestRoom.MyHasVisited);
    }

    [Test]
    public void TestSetDoors()
    {
        List<GameObject> testDoors = new List<GameObject>();
        myTestRoom.MyDoors = testDoors;
        Assert.AreEqual(testDoors, myTestRoom.MyDoors);
    }

    [Test]
    public void TestNotEqual()
    {
        Room other = new GameObject().AddComponent<Room>();
        other.MyRow = 1;
        other.MyCol = 2;
        Assert.False(myTestRoom.Equals(other));
    }

    [Test]
    public void TestEquals()
    {
        Room other = new GameObject().AddComponent<Room>();
        other.MyRow = 1;
        other.MyCol = 1;
        Assert.True(myTestRoom.Equals(other));
    }
}
