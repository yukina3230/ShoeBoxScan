INSERT INTO DATA_IMPORT_SHOE_BOX
(
    IM_SERIAL_KEY,
    PO_ID,
    UPC,
    CUSTOMER_SIZE,
    TOTAL_QTY,
    USER_ID,
    IMPORT_DATE,
    STATUS
)
VALUES
(
    :serial_key,
    :po_id,
    :upc,
    :customer_size,
    :total_qty,
    :user_id,
    to_date(:import_date, 'dd/MM/yyyy'),
    :status
)