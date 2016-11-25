using System;
using System.Runtime.InteropServices;
using Microsoft.DirectX.Direct3D;
using GraphicsStream=Microsoft.DirectX.GraphicsStream;

namespace NifViewer {
    public static class HeightMapGen {
        private struct Vector3 { public byte X, Y, Z; public override string ToString() { return ""+X+", "+Y+", "+Z; } }

        public static int Iterations=128;
        public static float Scale=0.0625f;

        private static Texture ConvertToARGB(Texture In) {
            Texture Out=TextureLoader.FromStream(BasicHLSL.Device, TextureLoader.SaveToStream(ImageFileFormat.Dds, In), 0, 0, 1,
                Usage.None, Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Box, 0);

            return Out;
        }

        private static Texture ConvertToRGB(Texture In, int Width, int Height) {
            Texture Out=TextureLoader.FromStream(BasicHLSL.Device, TextureLoader.SaveToStream(ImageFileFormat.Dds, In), Width, Height, 1,
                Usage.None, Format.R8G8B8, Pool.Scratch, Filter.Triangle|Filter.Dither, Filter.Box, 0);

            return Out;
        }

        private static Texture ConvertToDXT3(Texture In) {
            Texture Out=TextureLoader.FromStream(BasicHLSL.Device, TextureLoader.SaveToStream(ImageFileFormat.Dds, In), 0, 0, 0,
                Usage.None, Format.Dxt3, Pool.Managed, Filter.Point, Filter.Box, 0);

            return Out;
        }

        private static float CalcHeight(float In) {
            return 1/(float)Math.Tan(Math.Acos(In));
        }

        private static byte[,] GenHeightmap(Vector3[,] _normals, int width, int height) {
            float[,] heights=new float[width, height];
            float[,] newheights=new float[width, height];
            float[,] tmpheights;
            float min=float.MaxValue;
            float max=float.MinValue;

            Microsoft.DirectX.Vector3[,] normals=new Microsoft.DirectX.Vector3[width, height];
            for(int x=0;x<width;x++) {
                for(int y=0;y<height;y++) {
                    normals[x, y].X=(_normals[x, y].X / 255.0f) * 2 - 1;
                    normals[x, y].Y=(_normals[x, y].Y / 255.0f) * 2 - 1;
                    normals[x, y].Z=(_normals[x, y].Z / 255.0f) * 2 - 1;

                    normals[x, y].Y=(normals[x, y].Y)/(normals[x, y].X);
                    normals[x, y].Z=(normals[x, y].Z)/(normals[x, y].X);

                    //normals[x, y].Normalize();

                    //normals[x, y].Y=CalcHeight(normals[x, y].Y);
                    //normals[x, y].Z=CalcHeight(normals[x, y].Z);
                    //Microsoft.DirectX.Vector2 v2=new Microsoft.DirectX.Vector2(normals[x, y].X, normals[x, y].Y);
                    //v2.Normalize();
                    //normals[x, y].Y=CalcHeight(v2.Y);
                    //v2=new Microsoft.DirectX.Vector2(normals[x, y].X, normals[x, y].Z);
                    //v2.Normalize();
                    //normals[x, y].Z=CalcHeight(v2.Y);
                }
            }

            int cx= width/2;
            int cy = height/2;

            for(int x=cx+1;x<width;x++) heights[x, cy]=heights[x-1, cy]-normals[x-1, cy].Y;
            for(int x=cx-1;x>=0;x--) heights[x, cy]=heights[x+1, cy]+normals[x, cy].Y;
            for(int y=cy+1;y<height;y++) newheights[cx, y]=newheights[cx, y-1]-normals[cx, y-1].Z;
            for(int y=cy-1;y>=0;y--) newheights[cx, y]=newheights[cx, y+1]+normals[cx, y].Z;


            for(int y=cy+1;y<height;y++) {
                for(int x=0;x<width;x++) heights[x, y]=heights[x, y-1]-normals[x, y-1].Z;
            }
            for(int y=cy-1;y>=0;y--) {
                for(int x=0;x<width;x++) heights[x, y]=heights[x, y+1]+normals[x, y].Z;
            }
            for(int x=cx+1;x<width;x++) {
                for(int y=0;y<height;y++) newheights[x, y]=newheights[x-1, y]-normals[x-1, y].Y;
            }
            for(int x=cx-1;x>=0;x--) {
                for(int y=0;y<height;y++) newheights[x, y]=newheights[x+1, y]+normals[x, y].Y;
            }

            for(int x=0;x<width;x++) {
                for(int y=0;y<height;y++) {
                    heights[x, y]+=newheights[x, y];
                    heights[x, y]/=2;
                }
            }

            for(int iCount=0;iCount<Iterations;iCount++) {
                for(int x=0;x<width;x++) {
                    for(int y=0;y<height;y++) {
                        float expectedheight=0;

                        if(x!=0) expectedheight+=heights[x-1, y]-normals[x-1, y].Y;
                        else expectedheight+=heights[width-1, y]-normals[width-1, y].Y;

                        if(x!=width-1) expectedheight+=heights[x+1, y]+normals[x, y].Y;
                        else expectedheight+=heights[0, y]+normals[x, y].Y;

                        if(y!=0) expectedheight+=heights[x, y-1]-normals[x, y-1].Z;
                        else expectedheight+=heights[x, height-1]-normals[x, height-1].Z;

                        if(y!=height-1) expectedheight+=heights[x, y+1]+normals[x, y].Z;
                        else expectedheight+=heights[x, 0]+normals[x, y].Z;

                        newheights[x, y]=(heights[x, y] + (expectedheight*0.25f - heights[x, y])*Scale);
                    }
                }

                tmpheights=newheights;
                newheights=heights;
                heights=tmpheights;
            }

            for(int x=0;x<width;x++) {
                for(int y=0;y<height;y++) {
                    if(heights[x, y]>max) max=heights[x, y];
                    if(heights[x, y]<min) min=heights[x, y];
                }
            }

            byte[,] bHeights=new byte[width, height];
            for(int x=0;x<width;x++) {
                for(int y=0;y<height;y++) {
                    bHeights[x, y]=(byte)Math.Min((heights[x, y]-min)*256/(max-min), 255.0f);
                }
            }
            return bHeights;
        }

        private struct Coords {
            private int a, b;
            public int X { get { return a; } }
            public int Y { get { return b; } }

            public Coords(int A, int B) { a=A; b=B; }

            public static Coords operator++(Coords s) {
                if(s.b<s.a) {
                    s.b++;
                } else {
                    s.a++;
                    s.b=0;
                }
                return s;
            }

            public Coords[] Permiatate() {
                if(a==b) {
                    if(a==0) return new Coords[] { this };
                    else return new Coords[] { this, new Coords(-a, a), new Coords(a, -a), new Coords(-a, -a) };
                }
                if(b==0) return new Coords[] { this, new Coords(-a, 0), new Coords(0, a), new Coords(0, -a) };
                else return new Coords[] { this, new Coords(-a,b), new Coords(a, -b), new Coords(-a, -b),
                    new Coords(b,a), new Coords(-b,a), new Coords(b,-a), new Coords(-b,-a)};
            }
        }

        public static Texture CreateHeightmapData(Texture cMap, Texture nMap) {
            SurfaceDescription cDesc=cMap.GetLevelDescription(0);
            SurfaceDescription nDesc=nMap.GetLevelDescription(0);

            cMap=ConvertToARGB(cMap);
            nMap=ConvertToRGB(nMap, cDesc.Width, cDesc.Height);

            Vector3[,] normals=(Vector3[,])nMap.LockRectangle(typeof(Vector3), 0, LockFlags.ReadOnly, new int[] { cDesc.Width, cDesc.Height} );
            byte[,] heights=GenHeightmap(normals, cDesc.Width, cDesc.Height);
            nMap.UnlockRectangle(0);

            GraphicsStream gs=cMap.LockRectangle(0, LockFlags.None);
            for(int x=0;x<cDesc.Width;x++) {
                for(int y=0;y<cDesc.Height;y++) {
                    gs.Position+=3;
                    gs.Write(heights[x, y]);
                }
            }
            cMap.UnlockRectangle(0);

            cMap=ConvertToDXT3(cMap);

            return cMap;
        }
    }
}
