function FrameBuilder(){}
FrameBuilder.prototype = {
    _leftHand: undefined,
    _rightHand: undefined,

    withLeftHand: function(leftHand)
    {
        this._leftHand = leftHand;
        return this;
    },

    withRightHand: function(rightHand)
    {
        this._rightHand = rightHand;
        return this;
    },

    build: function()
    {
        var hands = [];
        
        if (this._leftHand)
        {
            this._leftHand.type = 'left';
            hands.push(this._leftHand);
        }

        if (this._rightHand)
        {
            this._rightHand.type = 'right';
            hands.push(this._rightHand);
        }
        return {
            hands: hands
        };
    }
};

module.exports = FrameBuilder;
