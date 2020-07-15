## Updating the upstream/aspnetcore/web.js directory

The contents of this directory come from https://github.com/dotnet/aspnetcore/ repo. I didn't want to use a real git
submodule because that's such a giant repo, and I only need a few files from it here. So instead I used the
`git read-tree` technique described at https://stackoverflow.com/a/30386041

One-time setup per working copy:

    git remote add -t master aspnetcore https://github.com/dotnet/aspnetcore.git

Then, to update the contents of upstream/aspnetcore/web.js to the latest:

    cd <directory containing this .md file>
    git rm -rf upstream/aspnetcore
    git fetch --tags --depth 1 aspnetcore
    git read-tree --prefix=src/Microsoft.MobileBlazorBindings.WebView.JS/upstream/aspnetcore/web.js -u refs/tags/v3.1.6:src/Components/Web.JS
    git commit -m "Get Web.JS files from tag v3.1.6"

Longer term, we may consider publishing Components.Web.JS as a NuGet package
with embedded .ts sources, so that it's possible to use inside a WebPack build
without needing to clone its sources.
