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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// opens a dialogue allowing the user to add maps to the file
        /// </summary>
        private void addMap_Click(object sender, EventArgs e)
        {
            //general file opening
            mainFile = new FileStream("../../../../Resources/master.rsrc", FileMode.Append, FileAccess.Write);
            writer = new BinaryWriter(mainFile);
            data = new OpenFileDialog();
            data.Title = "Open Map File";
            data.Filter = "Map Files|*.map";
            //checks if file opens successfully
            if (data.ShowDialog() == DialogResult.OK)
            {
                dataFile = File.OpenRead(data.FileName);
                reader = new BinaryReader(dataFile);
                
                
                //adds data from the file
                writer.Write("Map");
                writer.Write(data.FileName);
                for (int i = 0; i < 256; i++)
                    writer.Write(reader.ReadInt32());
                
                
                //closes file
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
            File.Delete("../../../../Resources/master.rsrc");
            mainFile = File.OpenWrite("../../../../Resources/master.rsrc");
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

            //file opening
            data = new OpenFileDialog();
            data.Title = "Open Data File";
            mainFile = File.OpenRead("../../../../Resources/master.rsrc");
            reader = new BinaryReader(mainFile);

            //checks if the file exists
            if (data.ShowDialog() == DialogResult.OK)
            {
                //loops until file is done
                while (reader.PeekChar() != -1)
                {
                    //switch to detect filetype
                    switch (reader.ReadString())
                    {
                        case "Map":
                            //checks if the path matches
                            if(data.FileName == reader.ReadString())
                            {
                                MessageBox.Show("File Located");
                                type = 1;
                                break;
                            }
                            //finishes passing through data 
                            for (int i = 0; i < 256; i++)
                            {
                                reader.ReadInt32();
                                line++;
                            }
                            break;
                    }
                    //breaks if a file was found
                    if (type != 0)
                        break;
                    line++;
                }
            }
            reader.Close();


            //rewrites the file, skipping the data to be deleted

            //opens up the new file and a temp file
            mainFile = File.OpenRead("../../../../Resources/master.rsrc");
            reader = new BinaryReader(mainFile);
            dataFile = File.OpenWrite("temp.bin");
            writer = new BinaryWriter(dataFile);
            int currentLine = 0;


            //adds all data before the data to be deleted
            while (currentLine < line)
            {
                //switch on file type
                switch (reader.ReadString())
                {
                    case "Map":
                        //same code as addMap
                        writer.Write("Map");
                        writer.Write(reader.ReadString());
                        for (int i = 0; i < 256; i++)
                        {
                            writer.Write(reader.ReadInt32());
                        }
                        //advances linecount
                        currentLine += 258;
                        break;
                }
            }

            //skips deleted data based on type variable
            switch (type)
            {
                case 1:
                    //skips 258 lines for maps
                    reader.ReadString();
                    reader.ReadString();
                    for(int i = 0; i<256; i++)
                    {
                        reader.ReadInt32();
                    }
                    break;
            }

            //adds data after deleted data

            //loops until file is done
            while (reader.PeekChar() != -1)
            {
                //switch based on filetype
                switch (reader.ReadString())
                {
                    case "Map":
                        //exact same code as addMap
                        writer.Write("Map");
                        writer.Write(reader.ReadString());
                        for (int i = 0; i < 256; i++)
                        {
                            writer.Write(reader.ReadInt32());
                        }
                        break;
                }
            }
            //closes and replaces files
            writer.Close();
            reader.Close();
            File.Replace("temp.bin", "../../../../Resources/master.rsrc", "resBack.bin");
        }

        /// <summary>
        /// shows all items in a message box
        /// </summary>
        private void viewItems_Click(object sender, EventArgs e)
        {
            //opens file and creates list
            List<string> items = new List<string>();
            mainFile = File.OpenRead("../../../../Resources/master.rsrc");
            reader = new BinaryReader(mainFile);
            string messageText = "";

            //loops until file is done
            while (reader.PeekChar() != -1)
            {
                //switch based on fileType
                switch (reader.ReadString())
                {
                    case "Map":
                        //adds the filename to the list
                        items.Add(reader.ReadString());
                        //passes through all data
                        for (int i = 0; i < 256; i++)
                            reader.ReadInt32();
                        break;
                }
            }
            //removes the beginnings of filenames for readability, works for all filetypes
            for(int i = 0; i<items.Count; i++)
            {
                items[i] = items[i].Split('\\')[items[i].Split('\\').Length - 1];
                messageText += items[i] + "\n";
            }
            //shows the list
            MessageBox.Show(messageText);
            reader.Close();
        }
    }
}
