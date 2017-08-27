using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2FMTester
{
    public partial class MainForm
    {
        public bool IsSora = false;
        
        public void BooleanSetter()
        {

            if (LoadLabel.Text == "File: P_EX100.mdlx")
            {
                IsSora = true;
            }
            else
            {
                IsSora = false;
            }

        }

        private void MDLXWriter()
        {
            if (IsSora == true)
            {
                int BasicalPointer1 = DMA_Access.ReadInteger(0x20341708) + 0x200007AC;
                int BasicalPointer2 = DMA_Access.ReadInteger(BasicalPointer1) + 0x20000028;
                int ColorPalletteLocation = DMA_Access.ReadInteger(BasicalPointer2) + 0x20022480;
                int BodyLocation = DMA_Access.ReadInteger(BasicalPointer2) + 0x20000480;
                int HeadLocation = DMA_Access.ReadInteger(BasicalPointer2) + 0x20010480;
                int FaceLocation = DMA_Access.ReadInteger(BasicalPointer2) + 0x200247D0;
                int CrownLocation = DMA_Access.ReadInteger(BasicalPointer2) + 0x200397D0;

                byte[] SoraColorPallette = File.ReadAllBytes(fileSelect.FileName).Skip(0x45280).Take(0x2350).ToArray();
                byte[] SoraBodyTexture = File.ReadAllBytes(fileSelect.FileName).Skip(0x23280).Take(0x10000).ToArray();
                byte[] SoraHeadTexture = File.ReadAllBytes(fileSelect.FileName).Skip(0x33280).Take(0x12000).ToArray();
                byte[] SoraFaceTexture = File.ReadAllBytes(fileSelect.FileName).Skip(0x475D0).Take(0x15000).ToArray();
                byte[] SoraCrownTexture = File.ReadAllBytes(fileSelect.FileName).Skip(0x5C5D0).Take(0xE22C).ToArray();

                DMA_Access.WriteBytes(BodyLocation, SoraBodyTexture);
                System.Threading.Thread.Sleep(10);
                DMA_Access.WriteBytes(ColorPalletteLocation, SoraColorPallette);
                System.Threading.Thread.Sleep(10);
                DMA_Access.WriteBytes(HeadLocation, SoraHeadTexture);
                System.Threading.Thread.Sleep(10);
                DMA_Access.WriteBytes(FaceLocation, SoraFaceTexture);
                System.Threading.Thread.Sleep(10);
                DMA_Access.WriteBytes(CrownLocation, SoraCrownTexture);

            }
        }
    }
}
