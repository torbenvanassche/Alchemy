public class Controls
{
    private static InputSettings input;
    public static InputSettings Input
    {
        get
        {
            if (input == null)
            {
                input = new InputSettings();
                input.Enable();
            }
            return input;
        }
    }
}