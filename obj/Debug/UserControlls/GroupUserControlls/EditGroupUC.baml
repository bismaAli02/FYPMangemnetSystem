   M S B A M L   `   `   `  ????N  JFYPManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?Eclr-namespace:FYPManagementSystem.UserControlls.StudentsUserControlls7FYPManagementSystem.UserControlls.StudentsUserControlls  5      F    @FYPManagementSystem.UserControlls.GroupUserControlls.EditGroupUC   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      WlocalEclr-namespace:FYPManagementSystem.UserControlls.StudentsUserControlls     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#???   5
      $??Regular"?5      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      
??????   MaterialDesignFont#???  5      .??5      #  MaterialDesignThemes.Wpf.Card  $??32q?6   $??-16 0q?6'   .??5   
   ? $??0,2,0,-2q?6    ? ColumnDefinitions 5      ?? 5      $??* ?6#   ?? 5      $??* ?6#    ? RowDefinitions 5      ?? 5      $
??Auto ?6    ?? 5      $
??Auto ?6    ?? 5      $
??Auto ?6    ?? 5      $
??Auto ?6    ?? 5      $
??Auto ?6    .+?5!      ?? $??\Assets\Images\group.png??6   $??1??66   $??0??6F   $??50??6S   ? 5#      $??1??6   $??0??6    $
??0 20q?60    5$      ?? 5%      $	??150 ?6'   ?? 5&      $??* ?6'   .+?5(      ?? $
??Left=?6   $??0??67   $??Projects Title??6G   $??Center=?6]   $	??100??6x   $??16(?6?   $??SemiBold"?6?   5)      ?? 5*      -   5        $??ProjectComboBox??5*   +   $??1??6   $
??Left=?6B   $
??10 0q?6]   $??Center=?6k   $	??140??6?   5+      ?? 5-      -   5         ??Name$ AssigProjButton??5-      $??1??6M   $??1??6]   $??20,35,70,35q?6j   $	??130??6?   $
??Left=?6?   .??5.      ??  ?? Orientation$ 
Horizontal=?6   .+?5/      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ 	Projector=?6,   $??0 2 10 2q?6=   ?? 50      -   5         ??Name$ AssignButtonTxt??50       $??Assign??69   $??Center=?6G   52      53      54      ? 56      $??2??6   $??0??6    $
??0 20q?60    57      ?? 58      $	??150 ?6'   ?? 59      $??* ?6'   .+?5;      ?? $
??Left=?6   $??Assigned Project??67   $??Bottom=?6O   $	??150??6j   $??16(?6w   $??SemiBold"?6?   $??0,0,0,3q?6?   ?? 5<      -   5        $??	APTextBox??5<   *   $??1??6    ?? 
IsReadOnly . $
??Left=?6M   $
??10 0q?6h   $??Center=?6v   $	??140??6?   5=      ? 5?      $??3??6   $??0??6    $
??0 20q?60    5@      ?? 5A      $	??150 ?6'   ?? 5B      $??* ?6'   .+?5D      ?? $
??Left=?6   $??0??67   $??Add Students??6G   $??Center=?6[   $	??100??6v   $??16(?6?   $??SemiBold"?6?   5E      ?? 5F      -   5        $??AddStuComboBox??5F   +   $??1??6   $
??Left=?6A   $
??10 0q?6\   $??Center=?6j   $	??140??6?   5G      ?? 5I      -   5        $ AddStuButton??5I      $??1??6F   $??3??6V   $??20,35,70,35q?6c   $	??130??6y   $
??Left=?6?   .??5J      ?? $ 
Horizontal=?6   .+?5K        $
 Plus=?6,   $??0 2 10 2q?68   ?? 5L      -   5        $ AddStuButtonTxt??5L       $	??Add??69   $??Center=?6D   5N      5O      5P      &   System.Windows.Controls.DataGrid  5Q      -   5        	  Name$	 GroupStudentDataGrid??5Q   $   $??4??6   
   CanUserAddRows
 .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .     
IsReadOnly .    AutoGenerateColumns .  $??2??6  $??0??6%     Columns 5R      4  .System.Windows.Controls.DataGridTemplateColumn  5S      ,  &System.Windows.Controls.DataGridColumn   Header$ Registration Number??6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6J      CellTemplate 5T      ?? 5U      .R?5V   "   ?? ??6,   ?? *RegNo+5W      5Y        5[      $
 Name??6-   $
 auto 6;    5\      ?? 5]      .R?5^   "   ?? ??6,   ?? *Name+5_      5a        5b      $ Status??6-   $
 auto 6=    5c      ?? 5d      .R?5e   "   d? -	   5         d? 	IsChecked$
 TrueY?5e   /    ? ToolTip$ Make Student In Active??6c   5f      5h      5j      5m      5n      5o      