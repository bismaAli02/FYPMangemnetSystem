   M S B A M L   `   `   `  ????N  JFYPManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FYPManagementSystemFYPManagementSystem  5      C    =FYPManagementSystem.UserControlls.AdvisorsUsercontrolls.AdvUC   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      /local!clr-namespace:FYPManagementSystem   5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#???   5
      $??Regular"?5      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      
??????   MaterialDesignFont#???  5      .??5      #  MaterialDesignThemes.Wpf.Card  $??32q?6   $??16q?6'   .??5   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    .+?5      ? $??0??6    5      ?? 5      $
??auto ?6$   ?? 5      $??* ?6$   .+?5      ?? -   5        $??AddAdvButton??5   d   $??0??6   $??Add Advisor??6&   $??Right=?6<   $	??130??6X   $??	472,0,0,0q?6?   &   System.Windows.Controls.DataGrid  5      -   5        $??advDataGrid??5   (   $??1??6      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .  	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$
 Name??61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6?      CellTemplate 5!      ?? 5"   "   .R?5#   &   ?? ??60   ?? *Name+5$   #   5&        5(      $ Designation??61   $
 auto 6F    5)      ?? 5*   "   .R?5+   &   ?? ??60   ?? *Designation+5,   #   5.        50      $ Salary??61   $
 auto 6A    51      ?? 52   "   .R?53   &   ?? ??60   ?? *Salary+54   #   56        58      $ Gender??61   $
 auto 6A    59      ?? 5:   "   .R?5;   &   ?? ??60   ?? *Gender+5<   #   5>        5@      $ Contact??61   $
 auto 6B    5A      ?? 5B   "   .R?5C   &   ?? ??60   ?? *	Contact+5D   #   5F        5H      $ Email??61   $
 auto 6@    5I      ?? 5J   "   .R?5K   &   ?? ??60   ?? *Email+5L   #   5N        5P      $ Date Of Birth??61   $
 auto 6H    5Q      ?? 5R   "   .R?5S   &   ?? ??60   ?? *DateOfBirth+5T   #   5V        5X      $
 Edit??61      
IsReadOnly . $
 auto 6Q    5Y      ?? 5Z   "   .R?5[   &   ??  ??Name$ 
EditButton??6-   -   5        
?????? 
?????? .??5\   *   .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ AccountEdit=?6@   
????  ?6S   5]   '   5^   #   5`      5b      ?? 5c      -   5         ??Name$ AddAdvScroll??5c   ,   $??1??6   $??Visible=?6B   $??Visible=?6j   .??5e      >? -   5         >?Name$ AddAdvUC??5e   #   $y?	Collapsed=?65   5f      5g      5j      5m      5n      