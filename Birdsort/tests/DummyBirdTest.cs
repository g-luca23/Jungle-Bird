using Xunit;
using System.Collections.Generic;


public class DummyBirdTests
{
    [Fact]
    public void Malus_ShouldInitializeCorrectly()
    {
        // Arrange
        var malus = new DummyBird.Malus();

        // Assert
        Assert.False(malus.check);
        Assert.False(malus.key);
        Assert.False(malus.cage);
        Assert.False(malus.bomb);
        Assert.False(malus.clock);
        Assert.False(malus.sleep);
    }

    [Fact]
    public void Malus_Setsleep_SetsSleepAndCheckToTrue()
    {
        // Arrange
        var malus = new DummyBird.Malus();

        // Act
        malus.setsleep();

        // Assert
        Assert.True(malus.check);
        Assert.True(malus.sleep);
        Assert.False(malus.cage);
        Assert.False(malus.key);
        Assert.False(malus.bomb);
        Assert.False(malus.clock);
    }

    [Fact]
    public void Malus_Setclock_SetsClockAndCheckToTrue()
    {
        // Arrange
        var malus = new DummyBird.Malus();

        // Act
        malus.setclock();

        // Assert
        Assert.True(malus.check);
        Assert.True(malus.clock);
        Assert.False(malus.cage);
        Assert.False(malus.key);
        Assert.False(malus.bomb);
        Assert.False(malus.sleep);
    }

    [Fact]
    public void Malus_Setkey_SetsKeyAndCheckToTrue()
    {
        // Arrange
        var malus = new DummyBird.Malus();

        // Act
        malus.setkey();

        // Assert
        Assert.True(malus.check);
        Assert.True(malus.key);
        Assert.False(malus.cage);
        Assert.False(malus.clock);
        Assert.False(malus.bomb);
        Assert.False(malus.sleep);
    }

    [Fact]
    public void Malus_Setcage_SetsCageAndCheckToTrue()
    {
        // Arrange
        var malus = new DummyBird.Malus();

        // Act
        malus.setcage();

        // Assert
        Assert.True(malus.check);
        Assert.True(malus.cage);
        Assert.False(malus.clock);
        Assert.False(malus.key);
        Assert.False(malus.bomb);
        Assert.False(malus.sleep);
    }
    [Fact]
    public void Malus_Setbomb_SetsBombAndCheckToTrue()
    {
        // Arrange
        var malus = new DummyBird.Malus();

        // Act
        malus.setbomb();

        // Assert
        Assert.True(malus.check);
        Assert.True(malus.bomb);
        Assert.False(malus.cage);
        Assert.False(malus.key);
        Assert.False(malus.clock);
        Assert.False(malus.sleep);
    }

    [Fact]
    public void Malus_Unset_ResetsAllValuesToFalse()
    {
        // Arrange
        var malus = new DummyBird.Malus();
        malus.setclock();
        // Act
        malus.unset();

        // Assert
        Assert.False(malus.check);
        Assert.False(malus.key);
        Assert.False(malus.cage);
        Assert.False(malus.bomb);
        Assert.False(malus.clock);
        Assert.False(malus.sleep);
    }

    [Fact]
    public void Malus_Sblocca_ShouldUnlockMalus()
    {
        // Arrange
        var malus = new DummyBird.Malus(check: true, cage: true, sleep: true);

        // Act
        malus.sblocca();

        // Assert
        Assert.False(malus.check);
        Assert.False(malus.cage);
        Assert.False(malus.sleep);
    }

    [Fact]
    public void SetMalus_ShouldSetCorrectMalus()
    {
        // Arrange
        var dummyBird = new DummyBird();

        // Act
        dummyBird.SetMalus("bomb");

        // Assert
        Assert.True(dummyBird.Modificatore.bomb);
        Assert.True(dummyBird.Modificatore.check);
        Assert.False(dummyBird.Modificatore.cage);
        Assert.False(dummyBird.Modificatore.key);
        Assert.False(dummyBird.Modificatore.clock);
        Assert.False(dummyBird.Modificatore.sleep);
    }

    [Fact]
    public void SameBirdsCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var branch = new DummyBranch();

        var dummyBird1 = new DummyBird { typeBird = "Red" };
        var dummyBird2 = new DummyBird { typeBird = "Red" };
        

        branch.stackBirdOn.Push(dummyBird1);
        branch.stackBirdOn.Push(dummyBird2);

        var dummyBird = new DummyBird { typeBird = "Red" };

        // Act
        int count = dummyBird.sameBirdsCount(branch);

        // Assert
        Assert.Equal(2, count);
    }

    [Fact]
    public void BranchCompleted_ShouldReturnTrue()
    {
        // Arrange
        var branch = new DummyBranch();


        var dummyBird1 = new DummyBird { typeBird = "Red" };
        var dummyBird2 = new DummyBird { typeBird = "Red" };

        branch.stackBirdOn.Push(dummyBird1);
        branch.stackBirdOn.Push(dummyBird2);


        branch.currentMaxspots = 2;

        // Act
        bool isCompleted = dummyBird2.branchCompleted(branch);

        // Assert
        Assert.True(isCompleted);
    }

    [Fact]
    public void BranchCompleted_ShouldReturnFalse()
    {
        // Arrange
        var branch = new DummyBranch();

        var dummyBird1 = new DummyBird { typeBird = "Red" };
        var dummyBird2 = new DummyBird { typeBird = "Blue" };

        branch.stackBirdOn.Push(dummyBird1);
        branch.stackBirdOn.Push(dummyBird2);


        branch.currentMaxspots = 2;

        // Act
        bool isCompleted = dummyBird2.branchCompleted(branch);

        // Assert
        Assert.False(isCompleted);
    }

    [Fact]
    public void RemoveMalus_ShouldRemoveAllBirds()
    {
        // Arrange
        var branch = new DummyBranch();

        var birdCage = new DummyBird();
        birdCage.branchLink = branch;
        birdCage.Modificatore.setcage();

        var birdSleep = new DummyBird();    
        birdSleep.branchLink = branch;
        birdSleep.Modificatore.setsleep();



        // Aggiungo gli uccelli alla lista
        branch.malusbirds.Add(birdCage);
        branch.malusbirds.Add(birdSleep);

        // Act
        birdCage.removemalus( key: true, clock: true);

        // Assert
        Assert.DoesNotContain(birdCage, branch.malusbirds); 
        Assert.DoesNotContain(birdSleep, branch.malusbirds); 
        Assert.False(birdCage.Modificatore.cage);
        Assert.False(birdSleep.Modificatore.sleep);
    }

        [Fact]
    public void RemoveMalus_ShouldRemoveSleepBird()
    {
        // Arrange
        var branch = new DummyBranch();

        var birdCage = new DummyBird();
        birdCage.branchLink = branch;
        birdCage.Modificatore.setcage();

        var birdSleep = new DummyBird();    
        birdSleep.branchLink = branch;
        birdSleep.Modificatore.setsleep();



        // Aggiungo gli uccelli alla lista
        branch.malusbirds.Add(birdCage);
        branch.malusbirds.Add(birdSleep);

        // Act
        birdCage.removemalus( key: false, clock: true);

        // Assert
        Assert.Contains(birdCage, branch.malusbirds); 
        Assert.DoesNotContain(birdSleep, branch.malusbirds);
        Assert.True(birdCage.Modificatore.cage);
        Assert.False(birdSleep.Modificatore.sleep);
    }

    [Fact]
    public void RemoveMalus_ShouldRemoveCageBird()
    {
        // Arrange
        var branch = new DummyBranch();

        var birdCage = new DummyBird();
        birdCage.branchLink = branch;
        birdCage.Modificatore.setcage();

        var birdSleep = new DummyBird();    
        birdSleep.branchLink = branch;
        birdSleep.Modificatore.setsleep();



        // Aggiungo gli uccelli alla lista
        branch.malusbirds.Add(birdCage);
        branch.malusbirds.Add(birdSleep);

        // Act
        birdCage.removemalus( key: true, clock: false);

        // Assert
        Assert.DoesNotContain(birdCage, branch.malusbirds); 
        Assert.Contains(birdSleep, branch.malusbirds);
        Assert.False(birdCage.Modificatore.cage);
        Assert.True(birdSleep.Modificatore.sleep);
    }

    [Fact]
    public void flyAway_ShouldRemoveAllBirdsFromStackAndList()
    {
        // Arrange
        var branch = new DummyBranch();

        var birdKey = new DummyBird();
        birdKey.branchLink = branch;
        birdKey.Modificatore.setkey();

        var birdClock = new DummyBird();
        birdClock.branchLink = branch;
        birdClock.Modificatore.setclock();

        var birdCage = new DummyBird();
        birdCage.branchLink = branch;
        birdCage.Modificatore.setcage();

        var birdSleep = new DummyBird();    
        birdSleep.branchLink = branch;
        birdSleep.Modificatore.setsleep();


        //aggiungo gli uccelli al ramo
        branch.stackBirdOn.Push(birdKey);
        branch.stackBirdOn.Push(birdClock);

        // Aggiungo gli uccelli alla lista
        branch.malusbirds.Add(birdCage);
        branch.malusbirds.Add(birdSleep);
    
        // Act
        birdClock.flyAway(birdClock.branchLink);

        // Assert
        Assert.DoesNotContain(birdClock, branch.stackBirdOn); 
        Assert.DoesNotContain(birdKey, branch.stackBirdOn); 
        Assert.False(birdKey.Modificatore.key);
        Assert.False(birdClock.Modificatore.clock);
        Assert.DoesNotContain(birdCage, branch.malusbirds); 
        Assert.DoesNotContain(birdSleep, branch.malusbirds); 
        Assert.False(birdCage.Modificatore.cage);
        Assert.False(birdSleep.Modificatore.sleep);
    }
    

}