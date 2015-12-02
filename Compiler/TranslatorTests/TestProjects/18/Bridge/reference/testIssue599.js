﻿/* global Bridge */

"use strict";

Bridge.define('TestIssue599.Issue599', {
    statics: {
        config: {
            init: function () {
                Bridge.ready(this.main);
            }
        },
        main: function () {
            var inst = new TestIssue599.Issue599();
        }
    },
    _something: "HI!",
    config: {
        init: function () {
            Bridge.ready(this.test, this);
        }
    },
    test: function () {
        Bridge.get(console).log(this._something);
    }
});

Bridge.init();