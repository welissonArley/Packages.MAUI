using MauiCode.Helpers.Extensions;
using MauiCode.Views.Components.CodeViewers.Base;
using MauiCode.Views.Components.CodeViewers.Hide;
using MauiCode.Views.Components.CodeViewers.Show;
using MauiCode.Views.Components.Keyboards;
using MauiCode.Views.Components.Keyboards.Base;
using MauiCode.Views.Pages;
using Packages.MAUI.App.Model.Enums;
using Packages.MAUI.App.ViewModels.PinCode;

namespace Packages.MAUI.App.Views.PinCode;

public class PinCodePage : CodePage, IQueryAttributable
{
	public PinCodePage(PinCodeViewModel viewModel)
	{
		BindingContext = viewModel;

		CallbackCodeFinished = viewModel.CodeEndedCommand;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Headline = query["headline"].ToString();

        SubHeadline = query["subHeadline"].ToString();

        Illustration = GetImage((IllustrationType)query["illustration"]);

        CodeViewer = GetCodeViewer((bool)query["showCode"], (bool)query["squareViewer"]);

        KeyboardViewer = GetKeyboard((KeyboardType)query["keyboardType"]);
    }

    private static BaseCodeViewer GetCodeViewer(bool showCode, bool squareViewer)
    {
        if (showCode && squareViewer)
        {
            return new SquareShowingCodeViewer
            {
                Size = 40,
                TextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                Color = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                FontSize = 25,
                CodeLength = 4,
                Margin = new Thickness(0, 0, 0, 40)
            };
        }

        if (!showCode && squareViewer)
        {
            return new SquareHidingCodeViewer
            {
                Size = 20,
                Color = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                CodeLength = 6,
                Margin = new Thickness(0, 0, 0, 40)
            };
        }

        if (showCode && !squareViewer)
        {
            return new CircleShowingCodeViewer
            {
                Size = 20,
                TextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                Color = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                FontSize = 25,
                CodeLength = 6,
                Margin = new Thickness(0, 0, 0, 40)
            };
        }

        return new CircleHidingCodeViewer
        {
            Size = 20,
            Color = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
            CodeLength = 6,
            Margin = new Thickness(0, 0, 0, 40)
        };
    }

    private static BaseKeyboardViewer GetKeyboard(KeyboardType keyboardType)
    {
        return keyboardType switch
        {
            KeyboardType.Circle => new KeyboardCircle
            {
                ShapeColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                CancelTextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                FontSize = 25,
                Size = 70,
                CancelTextFontSize = 18,
                TextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White
            },
            KeyboardType.NoShape => new KeyboardWithoutShape
            {
                CancelTextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                FontSize = 25,
                Size = 70,
                CancelTextFontSize = 18,
                TextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White
            },
            _ => new KeyboardSquare
            {
                ShapeColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                CancelTextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White,
                FontSize = 25,
                Size = 70,
                CancelTextFontSize = 18,
                TextColor = Application.Current.IsLightMode() ? Colors.White : Colors.Black
            }
        };
    }

    private static Image GetImage(IllustrationType illustrationType)
    {
        if (illustrationType == IllustrationType.None) return null;

        var path = illustrationType switch
        {
            IllustrationType.Bird => "bird.png",
            IllustrationType.Dog => "dog.png",
            _ => "turtle.png",
        };

        return new Image
        {
            Source = ImageSource.FromFile(path),
            HeightRequest = 80,
            Margin = new Thickness(0, 0, 0, 40),
        };
    }
}