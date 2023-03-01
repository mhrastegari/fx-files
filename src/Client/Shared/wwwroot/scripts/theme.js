const Theme = {
    dark: 'Dark',
    light: 'Light',
    system: 'System'
};

const themeKey = 'theme';
const dataThemeKey = 'data-theme';
const currentTheme = localStorage.getItem(themeKey);

const mediaDark = matchMedia('(prefers-color-scheme: dark)');
const mediaLight = matchMedia('(prefers-color-scheme: light)');

var FxTheme = {

    registerForSystemThemeChanged: (dotnetObj, callbackMethodName) => {
        if (dotnetObj) {
            const callback = (args) => {
                const isDark = args.matches;
                if (FxTheme.getTheme() === Theme.system) {
                    FxTheme.applyTheme(Theme.system);
                }
                dotnetObj.invokeMethod(callbackMethodName, isDark);
            };
            mediaDark.addEventListener('change', callback);
            mediaLight.addEventListener('change', callback);
        }
    },

    getSystemTheme: () =>
        matchMedia('(prefers-color-scheme: dark)').matches
            ? Theme.dark : Theme.light,

    getTheme: () => localStorage.getItem(themeKey),

    applyTheme: (theme) => {

        if (theme === Theme.system) {
            theme = FxTheme.getSystemTheme();
        }

        if (theme === Theme.dark) {
            document.body.setAttribute(dataThemeKey, theme);
        } else {
            document.body.removeAttribute(dataThemeKey);
        }
    },

    setTheme: (theme) => {
        FxTheme.applyTheme(theme);
        localStorage.setItem(themeKey, theme);
    }
};

FxTheme.applyTheme(currentTheme);