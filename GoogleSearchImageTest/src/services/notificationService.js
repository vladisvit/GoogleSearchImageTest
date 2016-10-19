(function ($) {
    'use strict';

    angular
        .module('apptest')
        .service('notificationService', notificationService);

    function notificationService() {
        var service = {
            roar: instantNotify,
            growl: notify,
            success: function (message) {
                return notify(message, { type: 'success' });
            },
            warning: function (message, autoClose) {
                var options = { type: 'warning' };

                if (!autoClose) {
                    options.delay = null;
                }

                return notify(message, options);
            },
            error: function (message, autoClose) {
                var options = { type: 'danger' };

                if (!autoClose) {
                    options.delay = null;
                }

                return notify(message, options);
            },
            info: function (message) {
                return notify(message, { type: 'info' });
            }
        };

        return service;

        function notify(message, options) {
            var defaults = {
                type: 'info',
                allow_dismiss: false,
                label: 'Cancel',
                className: 'btn-xs btn-inverse',
                placement: {
                    from: 'top',
                    align: 'right'
                },
                delay: 2000,
                animate: {
                    enter: 'animated bounceIn',
                    exit: 'animated bounceOut'
                },
                offset: {
                    x: 20,
                    y: 85
                },
                template: '<div data-notify="container" class="alert alert-{0}" role="alert"><button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div><a href="{3}" target="{4}" data-notify="url"></a></div>'
            };

            return $.notify({ message: message }, $.extend({}, defaults, options));
        }

        function instantNotify(objMessage, options) {
            $.notify(objMessage, options);
        }
    }
})(jQuery);