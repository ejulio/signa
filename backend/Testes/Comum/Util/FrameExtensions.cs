﻿using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Signa.Util;
using System.Linq;

namespace Testes.Comum.Util
{
    public static class FrameExtensions
    {
        public static double[] MontarArrayEsperado(this Frame frame)
        {
            return MontarArrayEsperadoParaAMao(frame.MaoEsquerda)
                .Concat(MontarArrayEsperadoParaAMao(frame.MaoDireita))
                .ToArray();
        }

        private static double[] MontarArrayEsperadoParaAMao(Mao mao)
        {
            var dadosDosDedos = mao.Dedos.Select(d =>
            {
                var tipo = new double[] { (int)d.Tipo };
                return tipo.Concat(d.Direcao).ToArray();
            }).Concatenar();

            return mao.VetorNormalDaPalma
                    .Concat(mao.DirecaoDaMao)
                    .Concat(dadosDosDedos)
                    .ToArray();
        } 
    }
}