
public class Resources {

    public Resources()
    {
        //baseline food level?
        Food = 10;
        Happiness = 100;
        Energy = 100;
        ShipHp = 100;
        Garbage = 0;
    }


    //Each prop can be extended to food being a Food object with expiry, etc
    public int Food { get; set; } // 0 to cap
    public int Happiness { get; set; } // 0 to 100
    public int Energy { get; set; } //0 to cap
    public int ShipHp { get; set; }// cap to 0
    public int Garbage { get; set; } // 0 to cap

}
