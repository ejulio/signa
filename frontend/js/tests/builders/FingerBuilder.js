function FingerBuilder(){}
FingerBuilder.FingerType = {
    THUMB: 0,
    INDEX: 1,
    MIDDLE: 2,
    RING: 3,
    PINKY: 4
};

FingerBuilder.prototype = {
    _direction: undefined,
    _type: FingerBuilder.FingerType.THUMB,

    withDirection: function(direction)
    {
        this._direction = direction;
        return this;
    },

    ofType: function(type)
    {
        this._type = type;
        return this;
    },

    build: function()
    {
        return {
            direction: this._direction,
            type: this._type
        }
    }
};

function _fingerOfType(type)
{
    var direction = Math.random() * type; // some magical random position
    return new FingerBuilder()
        .withDirection([direction, direction, direction])
        .ofType(type)
        .build();
}

FingerBuilder.thumb = function() { return _fingerOfType(FingerBuilder.FingerType.THUMB); };
FingerBuilder.index = function() { return _fingerOfType(FingerBuilder.FingerType.INDEX); };
FingerBuilder.middle = function() { return _fingerOfType(FingerBuilder.FingerType.MIDDLE); };
FingerBuilder.ring = function() { return _fingerOfType(FingerBuilder.FingerType.RING); };
FingerBuilder.pinky = function() { return _fingerOfType(FingerBuilder.FingerType.PINKY); };

module.exports = FingerBuilder;
