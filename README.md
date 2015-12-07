Let's take a look into some of the features and enhancements included in Xamarin 4.0 for developing cross-platform applications including Android, iOS and UWP (Universal Windows Apps). 

## Introducing Xamarin Forms 2.0

This time I made a simple app called *"Lucky Winner"* for randomly select a person from a list of participants using the commonly used *System.Random* class for random numbers just like any other .Net program. 

Here are some screenshots of the app running on different platforms:

![http://stvansolano.github.io/2015/12/01/Xamarin4-has-everything-you-need-to-create-great-mobile-apps screenshot]

You can download the full source for this app from my GitHub repository [here](https://github.com/stvansolano/lucky-winner).

## Developing a Xamarin.Forms app from scratch
By installing Xamarin Platform and geting a valid/trial license you will be able to create a PCL (Portable Class Library) that contains most of the code for you to be shared and compile native apps for Android, iOS and UWP. 

So let's pick the Xamarin.Forms template from the available list inside Visual Studio:

By doing so will get:
- A Xamarin.Forms project for sharing XAML/C# code between platforms
- Android project referencing Xamarin.Forms
- iOS project
- UWP project

If you are familiar with MVVM, XAML and C# it will be quite familiar to easily include Pages, ViewModels, Views in your project just like the following.

## Bring Material Design to your Android application 
Another great feature for Xamarin for Android is ability to easily incorporate *Material Design* into your Android apps. This can be done by adding the [Support Design Library](https://components.xamarin.com/gettingstarted/xamandroidsupportdesign) available as a package from NuGet.

## Additional resources & links.
[Introducing Xamarin 4](https://blog.xamarin.com/introducing-xamarin-4/)

[Adding Material Design to Xamarin Apps](https://blog.xamarin.com/introduction-to-android-material-design/)

[Lucky Winner source code](https://github.com/stvansolano/lucky-winner)
