   M S B A M L   `   `   `  ????N  JFYPManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FYPManagementSystemFYPManagementSystem  5      I    CFYPManagementSystem.UserControlls.AdvisorsUsercontrolls.AssignAdvUC   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      /local!clr-namespace:FYPManagementSystem   5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#???   5
      $??Regular"?5      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      
??????   MaterialDesignFont#???  5      .??5      #  MaterialDesignThemes.Wpf.Card  $??32q?6   $??16q?6'   .??5   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    .+?5      ? $??0??6    5      ?? 5      $
??auto ?6$   ?? 5      $??* ?6$   .+?5      ?? -   5        $??AssignProjectButton??5   r   $??0??6   $??Assign Advisor To Project??6&   $??Right=?6J   $	??200??6f   $??	472,0,0,0q?6?   &   System.Windows.Controls.DataGrid  5      -   5        $??AssignAdvDataGrid??5   (   $??1??6      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .  	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ Project Title??61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6H      CellTemplate 5!      ?? 5"   "   .R?5#   &   ?? ??60   ?? *Title+5$   #   5&        5(      $ Main Advisor??61   $
 auto 6G    5)      ?? 5*   "   .R?5+   &   ?? ??60   ?? *MainAdvisor+5,   #   5.        50      $ 
Co-Advisor??61   $
 auto 6E    51      ?? 52   "   .R?53   &   ?? ??60   ?? *	CoAdvisor+54   #   56        58      $ Industry Advisor??61   $
 auto 6K    59      ?? 5:   "   .R?5;   &   ?? ??60   ?? *IndustryAdvisor+5<   #   5>        5A      $
 Edit??61      
IsReadOnly . $
 auto 6Q    5B      ?? 5C   "   .R?5D   &   ??  ??Name$ 
EditButton??6-   -   5        
?????? 
?????? .??5E   *   .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ AccountEdit=?6@   
????+??6S   5F   '   5G   #   5I      5K      ?? 5L      -   5         ??Name$ AssignAdvScroll??5L      $??1??68   $y?	Collapsed=?6E   $??Visible=?6\   $??Visible=?6?   .??5M      >? -   5         >?Name$ addProjectUC??5M   #   $y?	Collapsed=?69   5O      5Q      5T      5W      5X      