# Thủ tục Kiểm thử Thủ công (Manual Test Procedure - L4)
**Yêu cầu:** US-FNB-04 (Quản lý Cửa hàng)
**Phụ trách:** QA Agent (Murph) / Manual Tester

Dưới đây là bộ Testcase đã được review và bổ sung để phản ánh đầy đủ các phát sinh sau khi tích hợp **Specification Rule Engine** ở tầng BLL. Các lỗi từ DB sẽ được dịch thành `Message` tiếng Việt bởi `StoreAppService`.

| Mã TC | Phân loại | Tên Kịch bản | Điều kiện tiên quyết | Các bước thực hiện | Kết quả mong đợi | Trạng thái |
|---|---|---|---|---|---|---|
| **TC-STORE-01** | Functional (Happy Path) | Thêm mới Cửa hàng thành công | Không có Cửa hàng nào trùng Mã | 1. Mở màn hình Quản lý Cửa hàng<br>2. Nhấn 'Thêm mới'<br>3. Nhập đầy đủ Mã, Tên, chọn Nhóm CH<br>4. Nhấn Lưu | Dữ liệu được lưu vào CSDL. Hiển thị popup Toast báo 'Lưu thành công', Grid tự động load lại và hiển thị dòng Cửa hàng mới. | `PENDING` |
| **TC-STORE-02** | Negative (Client-side) | Validation bắt lỗi trống dữ liệu (Tên & Mã) | Đang mở Form Thêm mới | 1. Bỏ trống ô Mã Cửa hàng và Tên Cửa hàng<br>2. Nhấn Lưu | Form **KHÔNG** gửi đi. Dưới ô input hiện chữ đỏ: 'Mã cửa hàng không được để trống' và 'Tên cửa hàng không được để trống'. (Bắt bởi DataAnnotations). | `PENDING` |
| **TC-STORE-03** | Negative (Server-side) | Validation lỗi trùng Mã Cửa hàng (Rule Engine) | Đã có Cửa hàng mã 'ST01' trong DB | 1. Mở Form Thêm mới<br>2. Nhập Mã 'ST01' và Tên bất kỳ<br>3. Nhấn Lưu | Hệ thống báo lỗi Toast: 'Mã Cửa hàng đã tồn tại.' (Dịch từ Code `STORE_NUMBER_EXISTS`). Form không bị đóng để user sửa lại. | `PENDING` |
| **TC-STORE-04** | Negative (Server-side) | Validation lỗi Cửa hàng không tồn tại khi Cập nhật/Xóa | Đang thao tác Cửa hàng bị xóa bởi người khác | 1. Mở form Edit của Cửa hàng A.<br>2. Trong lúc đó Cửa hàng A bị xóa dưới DB.<br>3. Nhấn Lưu trên form Edit. | Hệ thống chặn và báo lỗi Toast: 'Không tìm thấy Cửa hàng.' (Dịch từ Code `STORE_NOT_FOUND`). Dữ liệu không bị ghi đè sai. | `PENDING` |
| **TC-STORE-05** | Functional (UI/UX) | Popup Thêm nhanh Nhóm Cửa hàng | Đang nhập dở dữ liệu Form Thêm Cửa hàng | 1. Nhấn nút Add (+) kế bên Dropdown Nhóm | Hiển thị Dialog Popup Thêm Nhóm Cửa Hàng đè lên Form hiện tại. Các ô text trên form gốc không bị mất dữ liệu. | `PENDING` |
| **TC-STORE-06** | Functional (Auto-Fill) | Lưu thành công Nhóm Cửa hàng từ Popup | Đang ở trong Popup Thêm Nhóm | 1. Nhập Tên nhóm mới<br>2. Nhấn Lưu trên Popup | Popup đóng lại, Toast hiện 'Thêm nhóm thành công'. Dropdown ở Form Cửa hàng gốc tự reload danh sách và chọn sẵn Nhóm vừa tạo. | `PENDING` |
| **TC-STORE-07** | Negative (Server-side) | Lỗi bỏ trống tên Nhóm Cửa hàng | Đang ở trong Popup Thêm Nhóm | 1. Bỏ trống tên Nhóm<br>2. Bỏ qua Client Validator bằng API trực tiếp hoặc bug UI<br>3. Gửi Request | BLL chặn lại và báo Toast: 'Tên Nhóm Cửa hàng không được để trống.' (Mã `STOREGROUP_NAME_EMPTY`). | `PENDING` |

---
## Hướng dẫn thực hiện Manual Test:
1. Mở terminal và chạy lệnh `dotnet run` trong thư mục `FnBReport.GUI`.
2. Mở trình duyệt truy cập vào địa chỉ localhost của ứng dụng.
3. Điều hướng tới Menu **Quản lý Cửa hàng** (`/stores`).
4. Hãy đóng vai một User bình thường và thực hiện chính xác các thao tác theo cột **Các bước thực hiện**.
5. Quan sát Toast Message góc phải màn hình, đối chiếu với cột **Kết quả mong đợi**.
6. Ghi nhận Passed/Failed.
