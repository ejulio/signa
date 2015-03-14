function MaoBuilder(){}
MaoBuilder.prototype = {
    _direcao: undefined,
    _vetorNormalDaPalma: undefined,
    _dedos: undefined,

    comDirecao: function(direcao)
    {
        this._direcao = direcao;
        return this;
    },

    comVetorNormalDaPalma: function(vetorNormalDaPalma)
    {
        this._vetorNormalDaPalma = vetorNormalDaPalma;
        return this;
    },

    comDedos: function(dedos)
    {
        this._dedos = dedos;
        return this;
    },

    construir: function()
    {
        return {
            palmNormal: this._vetorNormalDaPalma,
            direction: this._direcao,
            fingers: this._dedos
        };
    }
};

module.exports = MaoBuilder;
