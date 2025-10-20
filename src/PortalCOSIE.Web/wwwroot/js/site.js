// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

class GlobalLoader {
    static requests = 0;

    static show() {
        this.requests++;
        $('#globalLoader')
            .removeClass('d-none')
            .addClass('d-flex')
            .show(); // Por si acaso
    }

    static hide() {
        this.requests = Math.max(0, this.requests - 1);
        if (this.requests === 0) {
            $('#globalLoader')
                .removeClass('d-flex')
                .addClass('d-none')
                .hide(); // Por si acaso
        }
    }

    static init() {
        $(document).ajaxStart(() => GlobalLoader.show());
        $(document).ajaxStop(() => GlobalLoader.hide());
        $(document).ajaxError(() => GlobalLoader.hide());
    }
}

$(document).ready(() => GlobalLoader.init());