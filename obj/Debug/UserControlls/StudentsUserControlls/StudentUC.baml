   M S B A M L   `   `   `  ????N  JFYPManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FYPManagementSystemFYPManagementSystem  5      #    FYPManagementSystem.StudentUC   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      /local!clr-namespace:FYPManagementSystem   5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#???   5
      $??Regular"?5      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      
??????   MaterialDesignFont#???  5      .??5      #  MaterialDesignThemes.Wpf.Card  $??32q?6   $??16q?6'   .??5   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    .+?5      ? $??0??6    5      ?? 5      $
??auto ?6$   ?? 5      $??* ?6$   .+?5      ?? -   5        $??AddStuButton??5   d   $??0??6   $??Add Student??6&   $??Right=?6<   $	??130??6X   $??	472,0,0,0q?6?   &   System.Windows.Controls.DataGrid  5      -   5        $??stuDataGrid??5   (   $??1??6      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .  	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Registration No.??61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6K      CellTemplate 5#      ?? 5$   "   .R?5%   &   ?? ??60   ?? *RegistrationNo+5&   #   5(        5+      $
 Name??61   $
 auto 6?    5,      ?? 5-   "   .R?5.   &   ?? ??60   ?? *Name+5/   #   51        53      $ Gender??61   $
 auto 6A    54      ?? 55   "   .R?56   &   ?? ??60   ?? *Gender+57   #   59        5;      $ Contact??61   $
 auto 6B    5<      ?? 5=   "   .R?5>   &   ?? ??60   ?? *	Contact+5?   #   5A        5C      $ Email??61   $
 auto 6@    5D      ?? 5E   "   .R?5F   &   ?? ??60   ?? *Email+5G   #   5I        5K      $ Date Of Birth??61   $
 auto 6H    5L      ?? 5M   "   .R?5N   &   ?? ??60   ?? *DateOfBirth+5O   #   5Q        5T      $
 Edit??61      
IsReadOnly . $
 auto 6Q    5U      ?? 5V   "   .R?5W   &   ??  ??Name$ 
EditButton??6I   -   5        $??Right=?5W   -   
?????? 
?????? .??5X   *   .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ AccountEdit=?6@   
????+??6S   5Y   '   5Z   #   5\      5^      ?? 5b      -   5         ??Name$ AddStuScroll??5b      $??1??65   $??Visible=?6B   $??Visible=?6j   .??5e      >? -   5         >?Name$ AddStuUC??5e   #   $y?	Collapsed=?65   5g      5h      5k      5n      5o      