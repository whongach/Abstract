using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//ResourceManager is a class that allows the user to add maps, weapons and enemies to the resource file that the game loads on launch
namespace ResourceManager
{
    public partial class Form1 : Form
    {
        //fields
        private BinaryReader reader;
        private Stream mainFile;
        private Stream dataFile;
        private BinaryWriter writer;
        private OpenFileDialog open;
        private OpenFileDialog data;


        //constructor
        //additionally opens the file to be saved to
        public Form1()
        {
            InitializeComponent();
            do
            {
                open = new OpenFileDialog();
                open.Title = "Open Resource File";
                open.Filter = "Resource Files|*.rsrc";
            } while (open.ShowDialog() != DialogResult.OK);
        }
        /// <summary>
        /// opens the necessary file and initializes the writer
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// opens a dialogue allowing the user to add maps to the file
        /// </summary>
        private void addMap_Click(object sender, EventArgs e)
        {
            mainFile = new FileStream(open.FileName, FileMode.Append, FileAccess.Write);
            writer = new BinaryWriter(mainFile);
            data = new OpenFileDialog();
            data.Title = "Open Map File";
            data.Filter = "Map Files|*.map";
            if (data.ShowDialog() == DialogResult.OK)
            {
                dataFile = File.OpenRead(data.FileName);
                reader = new BinaryReader(dataFile);
                writer.Write("Map");
                writer.Write(data.FileName);
                for (int i = 0; i < 256; i++)
                    writer.Write(reader.ReadInt32());
                dataFile.Close();
                MessageBox.Show("Map Added Successfully");
            }
            writer.Close();
        }

        /// <summary>
        /// opens a dialogue allowing the user to add enemies to the file
        /// </summary>
        private void addEnemy_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// opens a dialogue allowing the user to add weapons to the file
        /// </summary>
        private void addWeapon_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// deletes all data from the resource file
        /// </summary>
        private void resetData_Click(object sender, EventArgs e)
        {
            File.Delete(open.FileName);
            mainFile = File.OpenWrite(open.FileName);
            writer = new BinaryWriter(mainFile);
            writer.Close();
        }

        /// <summary>
        /// deletes a selected item from the resource file
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            //determines the line that the data to be deleted is one
            int line = 0;
            int type = 0;
            data = new OpenFileDialog();
            data.Title = "Open Data File";
            mainFile = File.OpenRead(open.FileName);
            reader = new BinaryReader(mainFile);
            if (data.ShowDialog() == DialogResult.OK)
            {
                while (reader.PeekChar() != -1)
                {
                    switch (reader.ReadString())
                    {
                        case "Map":
                            if(data.FileName == reader.ReadString())
                            {
                                MessageBox.Show("File Located");
                                type = 1;
                                break;
                            }
                            for (int i = 0; i < 256; i++)
                            {
                                reader.ReadInt32();
                                line++;
                            }
                            break;
                    }
                    if (type != 0)
                        break;
                    line++;
                }
            }
            reader.Close();
            //rewrites the file, skipping the data to be deleted
            mainFile = File.OpenRead(open.FileName);
            reader = new BinaryReader(mainFile);
            dataFile = File.OpenWrite("temp.bin");
            writer = new BinaryWriter(dataFile);
            int currentLine = 0;
            //adds all data before the data to be deleted
            while (currentLine < line)
            {
                switch (reader.ReadString())
                {
                    case "Map":
                        writer.Write("Map");
                        writer.Write(reader.ReadString());
                        for (int i = 0; i < 256; i++)
                        {
                            writer.Write(reader.ReadInt32());
                        }
                        currentLine += 258;
                        break;
                }
            }
            //skips deleted data
            switch (type)
            {
                case 1:
                    reader.ReadString();
                    reader.ReadString();
                    for(int i = 0; i<256; i++)
                    {
                        reader.ReadInt32();
                    }
                    break;
            }
            //adds data after deleted data
            while (reader.PeekChar() != -1)
            {
                switch (reader.ReadString())
                {
                    case "Map":
                        writer.Write("Map");
                        writer.Write(reader.ReadString());
                        for (int i = 0; i < 256; i++)
                        {
                            writer.Write(reader.ReadInt32());
                        }
                        break;
                }
            }
            writer.Close();
            reader.Close();
            File.Replace("temp.bin", open.FileName, "resBack.bin");
        }

        /// <summary>
        /// shows all items in a message box
        /// </summary>
        private void viewItems_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            mainFile = File.OpenRead(open.FileName);
            reader = new BinaryReader(mainFile);
            string messageText = "";
            while (reader.PeekChar() != -1)
            {
                switch (reader.ReadString())
                {
                    case "Map":
                        items.Add(reader.ReadString());
                        for (int i = 0; i < 256; i++)
                            reader.ReadInt32();
                        break;
                }
            }
            for(int i = 0; i<items.Count; i++)
            {
                items[i] = items[i].Split('\\')[items[i].Split('\\').Length - 1];
                messageText += items[i] + "\n";
            }
            MessageBox.Show(messageText);
            reader.Close();
        }
    }
}
