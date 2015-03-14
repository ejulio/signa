;(function(global, Signa, undefined)
{
    'use strict';

    function InformacoesDoFrame(){}
    InformacoesDoFrame.prototype = {
        extrairParaAmostra: function(frame)
        {
            return {
                MaoEsquerda: this._extrairDadosDaMaoEsquerda(frame.hands),
                MaoDireita: this._extrairDadosDaMaoDireita(frame.hands)
            };
        },

        _extrairDadosDaMaoEsquerda: function(maos)
        {
            if (this._ehMaoEsquerda(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoEsquerda(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoEsquerda: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'LEFT';
        },

        _extrairDadosDaMaoDireita: function(maos)
        {
            if (this._ehMaoDireita(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoDireita(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoDireita: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'RIGHT';
        },

        _extrairDadosDaMao: function(leapHand)
        {
            return {
                VetorNormalDaPalma: leapHand.palmNormal,
                Direcao: leapHand.direction,
                Dedos: this._extrairDadosDosDedos(leapHand.fingers)
            };
        },

        _extrairDadosDosDedos: function(leapFingers)
        {
            var dedos = new Array(leapFingers.length);

            for (var i = 0; i < dedos.length; i++)
            {
                dedos[i] = {
                    Tipo: leapFingers[i].type,
                    Direcao: leapFingers[i].direction
                };
            }

            return dedos;
        }
    };

    Signa.reconhecimento.InformacoesDoFrame = InformacoesDoFrame;
})(global = typeof global === 'undefined' ? window : global, global.Signa);
