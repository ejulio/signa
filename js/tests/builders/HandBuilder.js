function HandBuilder(){}
HandBuilder.prototype = {
    _direction: undefined,
    _palmNormal: undefined,
    _fingers: undefined,

    withDirection: function(direction)
    {
        this._direction = direction;
        return this;
    },

    withPalmNormal: function(palmNormal)
    {
        this._palmNormal = palmNormal;
        return this;
    },

    withFingers: function(fingers)
    {
        this._fingers = fingers;
        return this;
    },

    build: function()
    {
        return {
            palmNormal: this._palmNormal,
            direction: this._direction,
            fingers: this._fingers
        };
    }
};

module.exports = HandBuilder;
