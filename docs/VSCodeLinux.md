# Using VSCode on Linux

If you are using Ubuntu/Debian you may run into an issue where the application doesn't launch from inside VSCode. This includes trying to start the app via the terminal or using the debugger.

There are two environment variables injected by VSCode that cause the app to crash on launch. The easiest way to fix this is to override the two env vars in your VSCode user settings.

1. Launch VSCode
2. Press `Ctrl+Shift+P` and search for "settings json"
3. Select `Preferences: Open Settings (JSON)`
4. Paste this snippet and save the file:

```js
// Fix for OpenChart
"terminal.integrated.env.linux": {
    // https://github.com/OpenChartProject/OpenChart/issues/45
    "GDK_PIXBUF_MODULE_FILE": null,
    // https://github.com/OpenChartProject/OpenChart/issues/47
    "XDG_RUNTIME_DIR": "/run/user/1000",
},
```

**NOTE:** The `1000` in the `XDG_RUNTIME_DIR` path must match your user ID. To get your user ID run `echo $UID`
