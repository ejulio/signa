function FrameBuilder(){}
FrameBuilder.prototype = {
    _maoEsquerda: undefined,
    _maoDireita: undefined,

    comMaoEsquerda: function(maoEsquerda)
    {
        this._maoEsquerda = maoEsquerda;
        return this;
    },

    comMaoDireita: function(maoDireita)
    {
        this._maoDireita = maoDireita;
        return this;
    },

    construir: function()
    {
        var maos = [];
        
        if (this._maoEsquerda)
        {
            this._maoEsquerda.type = 'left';
            maos.push(this._maoEsquerda);
        }

        if (this._maoDireita)
        {
            this._maoDireita.type = 'right';
            maos.push(this._maoDireita);
        }
        return {
            hands: maos
        };
    }
};

module.exports = FrameBuilder;
