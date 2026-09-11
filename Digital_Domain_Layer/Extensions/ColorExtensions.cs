namespace Digital_Domain_Layer.Extensions;
using Digital_Domain_Layer.Enums;
public static class ColorExtensions
{
    public static string GetDescription(this Colors color) => color switch
    {
        Colors.Red    => "Red",
        Colors.Green  => "Green",
        Colors.Blue   => "Blue",
        Colors.Yellow => "Yellow",
        Colors.Orange => "Orange",
        Colors.Purple => "Purple",
        Colors.Brown  => "Brown",
        Colors.Gray   => "Gray",
        Colors.White  => "White",
        Colors.Black  => "Black",
        _             => color.ToString()
    };
}