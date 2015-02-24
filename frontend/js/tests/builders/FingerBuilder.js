function FingerBuilder(){}
FingerBuilder.prototype = {
    _tipPosition: undefined,

    withTipPosition: function(tipPosition)
    {
        this._tipPosition = tipPosition;
        return this;
    },

    build: function()
    {
        return {
            tipPosition: this._tipPosition
        }
    }
};

module.exports = FingerBuilder;
