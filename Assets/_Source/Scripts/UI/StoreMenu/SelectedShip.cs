public static class SelectedShip
{
    public static int NumberShip { get; private set; }

    public static void Select(int numberShip)
    {
        NumberShip = numberShip;
    }
}
