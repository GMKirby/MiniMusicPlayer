   M S B A M L   `   `   `  ����J  FMiniMusicPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MiniMusicPlayer.ViewModelsMiniMusicPlayer.ViewModels  5   	   +    %MiniMusicPlayer.Views.MusicPlayerView   5   	   e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ?
ViewModels(clr-namespace:MiniMusicPlayer.ViewModels     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Music Player��5   	   $	��600��5   	   $	��600��5	   	   $	��600��5
   	    � 
ResizeMode$ NoResize=�5   	    � SizeToContent$ WidthAndHeight=�5   	    � 	Resources 5         SymbolThemeFontFamily�� 5   
   %3   &	        *�5      Segoe MDL2 Assets)�6J   5       � DataContext 5      5   /MiniMusicPlayer.ViewModels.MusicPlayerViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $	��200 �6   �� 5      $	��100 �6    � ColumnDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      .+�5$   
   �� $��0��6   $��0��5%       �� Orientation$ Vertical=�5&      .+�5(      �� $��0��6   $��2��5)      $��0,5,15,5q�5*      $��Center=�5+      $��Artists��5,      �� 5.      $	��175��6   $	��175��5/      $
��Auto=�50      .��51      �� -   5         ��Name$ 
ArtistList��51      ��63   �� *
ArtistList+��52      �� 53      .R�54      �� $	��125��6%   $��5q�55   %   
������ $��0q�57   %   ��58   %   �� 	 �� ElementName$	 
ArtistList��
 �� Path$,
 &DataContext.FilterSongsByArtistCommand���59   %   �� .��5:   "   �� $	��125��6,    �� TextAlignment$ Center=�5<   ,   ��5;   ,   �� 5<   ,   5=      5>      5@      5A      5B      �� 5D   
   $��0��6   $��1��5E      $ Vertical=�5F      .+�5H      �� $��0��6   $��2��5I      $��	10,5,23,0q�5J      $��Center=�5K      ��5L      �� *SelectedArtist+�� 5N      $��0��6   $��1��5O      $	��175��5P      $��5q�5Q      $
��Auto=�5R      .��5S      �� -   5        $ SongList��5S      ��61   �� *CurrentSongList+��5T      �� 5U      .R�5V      �� $	��300��6%   $��5q�5W   %   
������ $��0q�5Y   %   ��5Z   %   �� $	 SongList��$!
 DataContext.PlaySongCommand���5[   %   �� *Path+.��5\   "   �� ��6,   �� *Title+5]      5^      5`      5a      5b      � 5d   
   $��2��6   $��2��5e      $��5q�5f       5g      �� 5h      �� 5i      .+�5l      �� $��0��6   $��Center=�5m      $	��Top=�5n      ��5o      �� *CurrentSongPlaying+� 5q      -   5         �Name$ ProgressBar��5q      $
��50,0q�5r      $��Center=�5s      $��5q�5t      
������ � IsIndeterminate .   � Minimum$ 0Q�5x       � Maximum 5w      �� * CurrentSongMaxTimeTotalSeconds+ � Value 5z      �� *!CurrentSongCurrentTimeInSeconds+ �� Mode$ TwoWay=��� 5|      $��0��6   $��50,0,0,0q�5}      $
��Left=�5~      $��Bottom=�5      ��5�      �� *CurrentSongCurrentTime+�� 5�      $��0��6   $��0,0,50,0q�5�      $��Right=�5�      $��Bottom=�5�      $ Right=�5�      ��5�      �� *CurrentSongMaxTimeString+5�      �� 5�      $��1��6   $��Center=�5�      $��Center=�5�      $ 
Horizontal=�5�      .+�5�      �� $��0q�6   $��Center=�5�      
������ $��0q�5�      ��5�      �� *PlayPreviousSongCommand+.��5�      �� $��5q�6    $��Center=�5�       #��[  5�       $��22(�5�       $	����5�       5�      �� 5�      $��0q�6   $��Center=�5�      
������ $��0q�5�      ��5�      �� *PlaySongCommand+y�5�      �� *PlayButtonVisibility+.��5�      �� $��5q�6    $��Center=�5�       #��[  5�       $��22(�5�       $	����5�       5�      �� 5�      $��0q�6   $��Center=�5�      
������ $��0q�5�      ��5�      �� *PauseSongCommand+y�5�      �� *PauseButtonVisibility+.��5�      �� $��5q�6    $��Center=�5�       #��[  5�       $��22(�5�       $	����5�       5�      �� 5�      $��0q�6   $��Center=�5�      
������ $��0q�5�      ��5�      �� *PlayNextSongCommand+.��5�      �� $��5q�6    $��Center=�5�       #��[  5�       $��22(�5�       $	����5�       5�      �� 5�      $��0q�6   $��Center=�5�      
������ $��0q�5�      ��5�      �� *ChangePlaybackModeCommand+y�5�      �� *#!PlaybackContinousButtonVisibility+.��5�      �� $��5q�6    $��Center=�5�       #��[  5�       $��22(�5�       $	����5�       5�      �� 5�      $��0q�6   $��Center=�5�      
������ $��0q�5�      ��5�      �� *ChangePlaybackModeCommand+y�5�      �� * PlaybackLoopedButtonVisibility+.��5�      �� $��5q�6    $��Center=�5�       #��[  5�       $��22(�5�       $	����5�       5�      5�      5�      5�      5�      