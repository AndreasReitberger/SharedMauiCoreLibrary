# SharedMauiCoreLibrary
A shared library containing recurring features &amp; utilities for .NET MAUI applications

## Available content
Please find a list of available content below.

### Behaviors

#### Namespace
```xaml
xmlns:behaviors="clr-namespace:AndreasReitberger.Shared.Core.Behaviors;assembly=SharedMauiCoreLibrary"
```

#### EventToCommandBehavior
This `Behavior` executes a `Command` when the specified `Event` is fired.

```xaml
<sliders:SfSlider
    Margin="0,4"
    Minimum="0" Maximum="{Binding LimitFan}"
    StepSize="1"
    MinorTicksPerInterval="100"
    ShowLabels="True"
    >
    <sliders:SfSlider.Behaviors>
        <behaviors:EventToCommandBehavior
                EventName="ValueChangeEnd"
                Command="{Binding SetFanCommand}"
                />
    </sliders:SfSlider.Behaviors>
</sliders:SfSlider>
```

### Converters

#### Namespace
```xaml
xmlns:converters="clr-namespace:AndreasReitberger.Shared.Core.Converters;assembly=SharedMauiCoreLibrary"
```

#### BooleanReverseVisibilityConverter & BooleanVisibilityConverter
This `Converter` converts a `Boolean` into a reverse and non-reverse visibility (if `value` is `true`, it returns `false`)

```xaml
<Label
    Style="{StaticResource SmallLabelStyle}"                   
    TextColor="{DynamicResource Error}"
    Text="{x:Static localization:Strings.NotAvailableDots}"
    IsVisible="{Binding HasDoubleExtruder, Converter={StaticResource BooleanReverseVisibilityConverter}}"
    />
```                        

#### ByteArrayToImageConverter
This `Converter` converts a `byte[]` into an `Image`

```xaml
<Image 
    VerticalOptions="Start"
    Margin="{OnIdiom Phone='-4,0', Tablet='-62,0', Default='-4,0'}"
    Source="{Binding Thumbnail, Converter={StaticResource ByteArrayToImageConverter}}" 
    >
    <Image.Style>
        <Style TargetType="Image">
            <Setter Property="Aspect" Value="AspectFit"/>
            <Style.Triggers>
                <DataTrigger
            TargetType="Image"
            Binding="{Binding IsPortrait}"
            Value="False">
                    <Setter Property="Aspect" Value="AspectFill"/>
                </DataTrigger>
            </Style.Triggers>
        </Style>
    </Image.Style>
</Image>
```     

#### ColorToBlackWhiteConverter
This `Converter` converts a `Color` into `Colors.White` or `Colors.Black`. This helps to get the
accurate `oreground` for a colored `Background`.

```xaml
<Border
    BackgroundColor="{Binding HexCode, Converter={StaticResource StringToColorConverter}, Mode=OneWay}"
    >
    <Label 
        Text="{Binding HexCode}"
        TextColor="{Binding Source={RelativeSource AncestorType={x:Type Border}}, Path=BackgroundColor, Converter={StaticResource ColorToBlackWhiteConverter}, Mode=OneWay}"
        Style="{StaticResource LabelStyle}"
        VerticalTextAlignment="Center"
        HorizontalTextAlignment="Center"
        />
  </Border>
```     


#### DoubleHoursToTimeSpanConverter
This `Converter` converts a `Double` into a `TimeSpan`.

```xaml
<Label 
    LineBreakMode="WordWrap" Margin="2,10,0,10"
    VerticalTextAlignment="Center"
    FontSize="{OnIdiom Tablet=14, Default=12}"
    >
    <Label.Style>
        <Style TargetType="Label" BasedOn="{StaticResource TitleViewHeadlineLabelStyle}">
            <Setter Property="IsVisible" Value="False"/>
            <Style.Triggers>
                <MultiTrigger
                    TargetType="Label">
                    <MultiTrigger.Conditions>
                        <BindingCondition Binding="{Binding IsPrinting, Mode=TwoWay}" Value="True"/>
                        <BindingCondition Binding="{Binding ShowRemainingPrintTimeInTitleView}" Value="True"/>
                    </MultiTrigger.Conditions>
                    <Setter Property="IsVisible" Value="True"/>
                </MultiTrigger>
            </Style.Triggers>
        </Style>
    </Label.Style>
    <Label.FormattedText>
        <FormattedString>
            <Span Text="("/>
            <Span Text="{Binding RemainingPrintTime, Converter={StaticResource DoubleHoursToTimeSpanConverter}}"/>
            <Span Text=")"/>
        </FormattedString>
    </Label.FormattedText>
</Label>
```     


#### DoubleHoursToTimeSpanConverter
This `Converter` converts a `List<string>` into a single `String` with the defined separator char.

#### LongToGigaByteConverter
This `Converter` converts a `long` into a `Double`. The result wil be in GigaBytes.

#### LongToMegaByteConverter
This `Converter` converts a `long` into a `Double`. The result wil be in MegaBytes.

#### StringToColorConverter
This `Converter` converts a `String` (hex formated string) into a `Color`.

#### UnixDateToDateTimeConverter
This `Converter` converts a `Double` (UNIX based) into a `DateTime`.

```xaml
<Label 
    LineBreakMode="WordWrap" Margin="2,10,0,10"
    VerticalTextAlignment="Center"
    FontSize="{OnIdiom Tablet=14, Default=12}"
    >
    <Label.FormattedText>
        <FormattedString>
            <Span Text="("/>
            <Span Text="{Binding CurrentDateTime, Converter={StaticResource UnixDateToDateTimeConverter}}"/>
            <Span Text=")"/>
        </FormattedString>
    </Label.FormattedText>
</Label>
```  

#### UnixDoubleHoursToTimeSpanConverter
This `Converter` converts a `Double` (UNIX based) into a `TimeSpan`.

```xaml
<Label 
    LineBreakMode="WordWrap" Margin="2,10,0,10"
    VerticalTextAlignment="Center"
    FontSize="{OnIdiom Tablet=14, Default=12}"
    >
    <Label.FormattedText>
        <FormattedString>
            <Span Text="("/>
            <Span Text="{Binding RemainingPrintTime, Converter={StaticResource UnixDoubleHoursToTimeSpanConverter}}"/>
            <Span Text=")"/>
        </FormattedString>
    </Label.FormattedText>
</Label>
```  

#### UriToStringConverter
This `Converter` converts a `Uri` into a `String`.

### Helpers

#### Namespace
```cs
namespace AndreasReitberger.Shared.Core
```

#### ViewModelBase
This `Class` contains all needed properties for a ViewModel. You can inherit from this Class directly for your ViewModel,
or create an own ViewModelBase and inherit there form this class.

```cs
public partial class BaseViewModel : ViewModelBase
    {
        #region Properties

        #region App
        bool _isBeta = SettingsStaticDefault.App_IsBeta;
        public new bool IsBeta
        {
            get { return _isBeta; }
            set { SetProperty(ref _isBeta, value); }
        }
        bool _isLightVersion = SettingsStaticDefault.App_IsLightVersion;
        public bool IsLightVersion
        {
            get { return _isLightVersion; }
            set { SetProperty(ref _isLightVersion, value); }
        }

        bool _isTabletMode = false;
        public bool IsTabletMode
        {
            get { return _isTabletMode; }
            set { SetProperty(ref _isTabletMode, value); }
        }
        #endregion

        #region Navigation
        bool _isViewShown = false;
        public bool IsViewShown
        {
            get { return _isViewShown; }
            set { SetProperty(ref _isViewShown, value); }
        }
        #endregion

        #region Connection
        bool _isConnecting = false;
        public bool IsConnecting
        {
            get { return _isConnecting; }
            set { SetProperty(ref _isConnecting, value); }
        }
        #endregion
        
        #endregion

        #region Constructor
        public BaseViewModel()
        {

        }
        #endregion

        #region LiveCycle
        public void OnDisappearing()
        {
            try
            {
                if (SettingsApp.SettingsChanged)
                {
                    // Notify other modules
                    MessagingCenter.Send(new AppMessageInfo(), AppMessages.OnSettingsChanged.ToString());
                    SettingsApp.SaveSettings();
                    SettingsApp.SettingsChanged = false;
                }
            }
            catch (Exception exc)
            {
                // Log error
                EventManager.Instance.LogError(exc);
            }
        }
        #endregion
    }
```

#### JsonConvertHelper
This `Class` uses Newtonsoft.JSON in order to serialize and deserialize objects to strings and vice reverse.
This is helpful if you want to store Collections or custom objects in the MAUI.Preferences (Settings).

```cs
// Convert to a String
SettingsApp.WebCam_DefaultWebCamSettings = JsonConvertHelper.ToSettingsString(value);

// Convert back to an object
ObservableCollection<KlipperWebCamSettingsInfo> webcams = JsonConvertHelper.ToObject(SettingsApp.WebCam_Settings, new ObservableCollection<KlipperWebCamSettingsInfo>());
```
