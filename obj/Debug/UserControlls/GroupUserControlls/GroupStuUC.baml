   M S B A M L   `   `   `  ????N  JFYPManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FYPManagementSystemFYPManagementSystem  5      E    ?FYPManagementSystem.UserControlls.GroupUserControlls.GroupStuUC   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      /local!clr-namespace:FYPManagementSystem   5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#???   5
      $??Regular"?5      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      
??????   MaterialDesignFont#???  5      .??5      #  MaterialDesignThemes.Wpf.Card  $??32q?6   $??16q?6'   .??5   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    .+?5      ? $??0??6    5      ?? 5      $
??auto ?6$   ?? 5      $??* ?6$   .+?5      ?? -   5        $??CreateGroupButton??5   e   $??0??6   $??Create Group??6&   $??Right=?6=   $	??130??6Y   $??	472,0,0,0q?6?   &   System.Windows.Controls.DataGrid  5      -   5        $??GroupDataGrid??5   (   $??1??6      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .  	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Group ID??61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6C      CellTemplate 5#      ?? 5$   "   .R?5%   &   ?? ??60   ?? *	GroupId+5&   #   5(        5+      $ Project Title??61   $
 auto 6H    5,      ?? 5-   "   .R?5.   &   ?? ??60   ?? *Title+5/   #   51        53      $ Total Students??61   $
 auto 6I    54      ?? 55   "   .R?56   &   ?? ??60   ?? *RegNo+57   #   59        5<      $ Remaining Students??61   $
 auto 6M    5=      ?? 5>   "   .R?5?   &   ?? ??60   ?? *RemainingStudents+5@   #   5B        5D      $ 
Created On??61   $
 auto 6E    5E      ?? 5F   "   .R?5G   &   ?? ??60   ?? *
Created_On+5H   #   5J        5L      $ Group Detail??61      
IsReadOnly . $
 auto 6Y    5M      ?? 5N   "   .R?5O   &   ??  ??Name$ GDButton??6I   -   5        $??Right=?5O   -   
?????? 
?????? .??5P   *   .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$
 Menu=?6@   
????+??6L   5Q   '   5R   #   5T        5W      $
 Edit??61    . $
 auto 6Q    5X      ?? 5Y   "   .R?5Z   &   ?? $ 
EditButton??6I   -   5        $??Right=?5Z   -   
?????? 
?????? .??5[   *     $ AccountEdit=?6@   
????+??6S   5\   '   5]   #   5_      5a      ?? 5b      -   5         ??Name$ GroupScroll??5b      $??1??64   $??Visible=?6A   $??Visible=?6i   $y?	Collapsed=?6?   .??5c      >? -   5         >?Name$ GroupUC??5c   #   $y?	Collapsed=?64   5e      5f      5i      5l      5m      