   M S B A M L   `   `   `  ����H  DNorthwindCRUD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NorthwindCRUDNorthwindCRUD  5          NorthwindCRUD.Delete   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:NorthwindCRUD     5        N� Title$  Delete��5	         negroBrocha N� 
Background# [  6   .��5      � 
������  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   � $��0��6   $��35, 35, 0, 10q�6!   .+�5      �� $��Images/Delete_verde.png��6   $��25��65   $��10q�6A   6O   �� 5      $��Center=�6   $��10q�65   $��Center=�6A   $��BORRAR��6\   $��18(�6j   $
��Bold"�6x     verdeBrocha#��[ 6�   6�   5      �� 5   
   $��1��6    �� Orientation$ Vertical=�6"   
������ .+�5      � $��5q�6   .+�5      �� $ Vertical=�6   $��80, 15, 0, 0q�64   .+�5      �� $��16(�6    $��DemiBold"�6.   $��Producto��6D   #��[ 6T   6�   �� 5      -   5        $��tbNombre��5   �   $��
0, 0, 0, 1q�6A   #��[ 6^   $
��Left=�6�   $	��200��6�   $��20��6�   
������   
grisBrocha#��[ 6�   6  5      �� 5      -   5         ��Name$ 	btnBorrar��5   �   $��Borrar��62   $��Medium"�6D   $	��120��6X   $��25��6d   $��50, 25, 0, 0q�6p     EstiloBotonVerde#��[ 6�   6�   5      �� 5      -   5         ��Name$ tbError��5   �    �� TextAlignment$ Center=�6   $0��*Este TextBlock sirve para mostrar mensajes��6/     
rojoBrocha#��[ 6a   $	��225��6�   $��20q�6�   6�   5      � 5    
   $��3��6   $��35, 0, 35, 15q�6    5!      �� 5"      $
��Auto �6    60   �� 5#      $��* �6    6-   .+�5&      &   System.Windows.Controls.DataGrid     HeadersVisibility$ Column=�6      AutoGenerateColumns .  $��0��5'      $��0,0,0,2q�6     CustomDataGrid#��[ 5(      	   Columns	 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn
   Header$
 	PRODUCTOS��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *ProductName+  5+      $
 
CATEGORÍA��6)    6<   �� *CategoryName+5-      �� 5/      $��1��6   #��[ 6"   $��1q�6M   ._�50        -   5        $��ProductosDataGrid��52   7    .  $
 None=�51      #��[ 52      	 53        54       6-   �� *ProductName+  55       6-   �� *CategoryName+57      58      59      5:      5;      