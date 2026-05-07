# Pixel Destruction Unity3D

## Demo
- Demo video gameplay level 1-5: https://drive.google.com/file/d/1YT380Ktrjuan3lx-wlGwyHtHvX0PmGoJ/view?usp=drive_link
- Thiết bị sử dụng để test gameplay: Redmi Note 9S Qualcomm Snapdragon 720G, 6GB RAM.
- APK Game: https://github.com/TLeTu/Pixel-Destruction-Unity3D/releases/tag/v.0.5
## Hướng dẫn chạy project
- Unity version: 6000.3.13f1
- Chạy MainScene, dữ liệu của level, blocks được lưu trong Resources, Game sẽ tự load các level trong Resources
- Để mở editor chọn Tools -> Level Editor
    + Đầu tiên mở scene EditorScene (EditorScene là một bản clone layout của MainScene)
    + Mở editor ở Tools -> Level Editor
    + Kế tiếp kéo một level config (level config là scriptable objects Create -> Game Config -> Level Config) vào ô Level Config, hoặc click 'Create New Level Config' để tạo một config mới. Các file level config và block data có sẵn được đặt trong Resources.
    + Sau đó chúng ta sẽ thấy các thông số của level config chúng ta có thể edit.
    + Ở Weapon Prefab, kéo prefab của weapon mà level sẽ sử dụng vào (Assets/Prefabs/Weapons), hiện tại chỉ có saw. Sau khi kéo vào chúng ta có thể tùy chỉnh thông số của weapon đó.
    + Ở phần block config chúng ta có thể chọn 'Add Empty Slot' để kéo một block data đã có sẵn vào (block data cũng là scriptable object Create -> Game Config -> Block Data), hoặc chọn 'Create new block data' để tạo một block mới, các block data này chính là các khối pixel block sẽ được spawn trong scene. Sprite là hình dạng của pixel block, kéo một file sprite 16x16 vào và khi khởi tạo pixel block sẽ có hình dạng của sprite đó. Pixel Health là máu của từng pixel.
    + Ở phần Obstacles Setting là nơi lưu các vị trí của các obstacle (cũng là nơi đặt weapon), kéo obstacle prefab vào ô Editor Obstacle Prefab (Assets/Prefabs/Weapons), ấn 'Add New Obstacle' editor sẽ tạo một obstacle trong scene và tọa độ của obstacle đó sẽ xuất hiện trong editor, khi ta kéo thả obstacle đó trong scene các tọa độ sẽ tự động cập nhật và lưu và level config.
    + Cuối cùng ấn save và thoát editor. File level config nên lưu vào Resources/Data levels, các file block data có thể lưu tùy ý, trở về mainscene và chạy game, game sẽ tự động load các level config trong Resources.
