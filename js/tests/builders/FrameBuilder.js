function FrameBuilder(){}
FrameBuilder.prototype = {
    _hands: undefined,

    withHands: function(hands)
    {
        this._hands = hands;
        return this;
    },

    build: function()
    {
        return {
            hands: this._hands
        };
    }
};

module.exports = FrameBuilder;
