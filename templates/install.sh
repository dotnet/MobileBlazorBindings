# /bin/bash
# install.sh

install=false
reset=false
package=false

reset_package() {
    echo "===Reset Installed Template==="
    echo ""

    dotnet new --debug:reinit > /dev/null

    echo "Removed user-defined templates."
    echo ""
}

make_package() {
    echo "===Packaging Templates==="
    echo ""

    nuget pack multiplatform/xamarin-templates-multiplatform.nuspec > /dev/null
    nuget pack ios/xamarin-templates-ios.nuspec > /dev/null
    nuget pack watchos/xamarin-templates-watchos.nuspec > /dev/null
    nuget pack tvos/xamarin-templates-tvos.nuspec > /dev/null
    nuget pack android/xamarin-templates-android.nuspec > /dev/null

    echo "Succesfully packaged templates."
    echo ""
}

install_package() {
    echo "===Installing Templates==="
    echo ""

    echo ""

    dotnet new --install $root/Xamarin.Templates.Multiplatform.*.nupkg > /dev/null
    dotnet new --install $root/Xamarin.Templates.iOS.*.nupkg > /dev/null
    dotnet new --install $root/Xamarin.Templates.watchOS.*.nupkg > /dev/null
    dotnet new --install $root/Xamarin.Templates.tvOS.*.nupkg > /dev/null
    dotnet new --install $root/Xamarin.Templates.Android.*.nupkg > /dev/null

    echo ""
    echo "Successfully installed templates."
    echo ""
}

while [[ $# > 0 ]]; do
    lowerI="$(echo $1 | awk '{print tolower($0)}')"
    case $lowerI in
        -r|--reset)
            reset=true
            ;;
        -i|--install)
            ;;
        -h|--help)
            echo "Options:"
            echo "  -r|--reset      Removes all user-defined templates."
            echo "  -i|--install    Install all user-defined templates."
            echo "  -p|--package    Package the user-defined templates in a nupkg."
            echo "  -h|--help       Display all commands."
            exit 0
            ;;
        *)
            break
            ;;
    esac

    shift
done

if [ "$install" = false ] &&  [ "$reset" = false ] && [ "$package" = false ]; then
    install=true
    reset=true
    package=true
fi

echo ""
echo "===Prerequisites==="

if [ "$OSTYPE" = "OSX" ]; then

    if [ ! -d /usr/local/share/dotnet/sdk ]; then
        echo "Hold up..."
        echo "  dotnet isn't installed, you must install dotnet core to use dotnet new command."
        echo "  Go do that and try again."
        exit 0
    fi

    echo ""
    echo "Found dotnet cli at /usr/local/share/dotnet."
    echo ""

elif [ "$OSTYPE" = "WINDOWS" ]; then

    if [ ! -d "C:\Program Files\dotnet\sdk" ]; then
        echo "Hold up..."
        echo "  dotnet isn't installed, you must install dotnet core to use dotnet new command."
        echo "  Go do that and try again."
        exit 0
    fi

    echo ""
    echo "Found dotnet cli at C:\Program Files\dotnet\sdk."
    echo ""

fi

root=$PWD

# --reset

if [ "$reset" = true ]; then
    reset_package
fi

# --package
if [ "$package" = true ]; then
    make_package
fi

# --install

if [ "$install" = true ]; then
    install_package
fi