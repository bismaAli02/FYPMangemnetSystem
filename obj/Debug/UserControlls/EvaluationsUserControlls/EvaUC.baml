   M S B A M L   `   `   `  ????N  JFYPManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FYPManagementSystemFYPManagementSystem  5      F    @FYPManagementSystem.UserControlls.EvaluationsUserControlls.EvaUC   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      /local!clr-namespace:FYPManagementSystem   5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#???   5
      $??Regular"?5      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      
??????   MaterialDesignFont#???  5      .??5      #  MaterialDesignThemes.Wpf.Card  $??32q?6   $??16q?6'   .??5   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    .+?5      ? $??0??6    5      ?? 5      $
??auto ?6$   ?? 5      $??* ?6$   .+?5      ?? -   5        $??AddEvaButton??5   g   $??0??6   $??Add Evaluation??6&   $??Right=?6?   $	??130??6[   $??	472,0,0,0q?6?   &   System.Windows.Controls.DataGrid  5      -   5        $??EvaDataGrid??5   (   $??1??6      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .  	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$
 Name??61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6?      CellTemplate 5!      ?? 5"   "   .R?5#   &   ?? ??60   ?? *Name+5$   #   5&        5(      $ Total Marks??61   $
 auto 6F    5)      ?? 5*   "   .R?5+   &   ?? ??60   ?? *
TotalMarks+5,   #   5.        50      $ Total Weightage??61   $
 auto 6J    51      ?? 52   "   .R?53   &   ?? ??60   ?? *TotalWeightage+54   #   56        59      $
 Edit??61      
IsReadOnly . $
 auto 6Q    5:      ?? 5;   "   .R?5<   &   ??  ??Name$ 
EditButton??6-   -   5        
?????? 
?????? .??5=   *   .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ AccountEdit=?6@   
????+??6S   5>   '   5?   #   5A      5C      >? 5D      -   5         >?Name$ AddEvaUC??5D   +   $??1??6   $y?	Collapsed=?6=   5E      5H      5K      5L      