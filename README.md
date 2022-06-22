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
This behavior executes a `Command` when the specified `Event` is fired.

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


| Function                            | Added?| Tested? |
| ----------------------------------- |:-----:| -------:|
| Get Klippy host information         | ✅   | ✅      |
| Emergency Stop                      | ✅   | ✅      |
| Host Restart                        | ✅   | ✅      |
| Firmware Restart                    | ✅   | ✅      |
