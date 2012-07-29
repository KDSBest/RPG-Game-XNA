using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RPG_Game_XNA;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using RPGData;

namespace MapEditor
{
    public partial class MapEditor : Form
    {
        public MapEditor()
        {
            InitializeComponent();
        }

        public static System.Drawing.Image Texture2Image(Texture2D texture)
        {
            if (texture == null)
            {
                return null;
            }

            if (texture.IsDisposed)
            {
                return null;
            }

            //Memory stream to store the bitmap data.
            MemoryStream ms = new MemoryStream();

            //Save the texture to the stream.
            texture.SaveAsPng(ms, texture.Width, texture.Height);

            //Seek the beginning of the stream.
            ms.Seek(0, SeekOrigin.Begin);

            //Create an image from a stream.
            System.Drawing.Image bmp2 = System.Drawing.Bitmap.FromStream(ms);

            //Close the stream, we nolonger need it.
            ms.Close();
            ms = null;
            return bmp2;
        }

        Image img;
        Pen pen;
        Graphics g;
        private void MapEditor_Load(object sender, EventArgs e)
        {
            img = Texture2Image(Globals.Instance.Content.Load<Texture2D>("ForestTiles"));
            pen = new Pen(Color.DarkRed, 4);
            pictureBox1.Image = img;
            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;
            g = pictureBox1.CreateGraphics();
            vScrollBar1.Maximum = img.Height / Globals.Instance.TileEngine.Map.TileSize.Y + 5;
            hScrollBar1.Maximum = img.Width / Globals.Instance.TileEngine.Map.TileSize.X + 5;
            g.DrawRectangle(pen, new Rectangle(0, 0, 64, 64));
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox1.Top = -vScrollBar1.Value * Globals.Instance.TileEngine.Map.TileSize.Y;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox1.Left = -hScrollBar1.Value * Globals.Instance.TileEngine.Map.TileSize.X;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            MouseEventArgs mea = (MouseEventArgs)e;
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            int XTiles = mea.X / Globals.Instance.TileEngine.Map.TileSize.X;
            int X = XTiles * Globals.Instance.TileEngine.Map.TileSize.X;
            int YTiles = mea.Y / Globals.Instance.TileEngine.Map.TileSize.Y;
            int Y = YTiles * Globals.Instance.TileEngine.Map.TileSize.Y;
            Globals.Instance.SelectedTile = XTiles + YTiles * Globals.Instance.TileEngine.Map.TilesPerRow;

            g.DrawRectangle(pen, new Rectangle(X, Y, 64, 64));
        }

        private void NewMap_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            map.AssetName = "NewMap";
            int SizeX = (int)this.SizeX.Value;
            int SizeY = (int)this.SizeY.Value;
            map.Name = "NewMap";
            map.MapDimensions = new Microsoft.Xna.Framework.Point(SizeX, SizeY);
            map.TileSize = new Microsoft.Xna.Framework.Point(64, 64);
            map.SpawnMapPosition = new Microsoft.Xna.Framework.Point(0, 0);

            map.TextureName = TileSet.Text;
            map.Texture = Globals.Instance.Content.Load<Texture2D>(TileSet.Text);
            map.TilesPerRow = map.Texture.Width / map.TileSize.X;

            map.BaseLayer = new int[SizeX * SizeY];
            map.FringeLayer = new int[SizeX * SizeY];
            map.ObjectLayer = new int[SizeX * SizeY];
            map.CollisionLayer = new int[SizeX * SizeY];
            for (int i = 0; i < SizeX * SizeY; i++)
            {
                map.BaseLayer[i] = map.FringeLayer[i] = map.ObjectLayer[i] = -1;
                map.CollisionLayer[i] = 0;
            }
            map.RandomFightPossibility = 0;
            Globals.Instance.TileEngine.SetMap(map, -1);
            img = Texture2Image(map.Texture);
            pictureBox1.Image = img;
            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;
            g = pictureBox1.CreateGraphics();
            vScrollBar1.Maximum = map.Texture.Height / Globals.Instance.TileEngine.Map.TileSize.Y + 5;
            hScrollBar1.Maximum = Globals.Instance.TileEngine.Map.TilesPerRow;
            vScrollBar1.Value = 0;
            hScrollBar1.Value = 0;
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            Globals.Instance.SelectedTile = 0;

        }
    }
}
