# FileManager
envs Requirement

1->Visual Studio 2019/.NET FrameWork4.7

2->SQL Server LocaldataDB 2017

Quick start

1.Download or Clone this Filemanager Repositories
    
2.Open the Visual Studio solutions "FileManager.sln"

3.For you to quickly figure out the project

All files are contained in folders FileManager.
First you should quickly figure out that each Form.cs contain Form.Designer and Form.resx corresponding to Form surface layout and surface resource, this solutions contain four Form.
Among them the Form1 is the main surface, in this surface we realize 

    1-> File data tree structure loading

    2-> Subfile node access

    3-> File comparison display

    4-> File retrieval based on keywords

    5-> Text editing and display

The Form2 was only provide for Load data and reports.

In order to ensure the solutions security,Form3 and Form4 was provided for the assign system.

4.In addition to, the TreeClass.cs Programming with Generics which Derives the return value type of a function from the file nodes type. we choose the 3 level file 
hierarchical structure. Traversing the file tree recursively, which the tree class defined by us is a tree node with three layers， for more detail you can read "Get the file tree structure recursively.md"

  
