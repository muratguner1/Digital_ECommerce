'use client';
import { useSelector, useDispatch } from "react-redux";
import ProductCard from './components/ProductCard';
import styles from './page.module.css';
import { useGetProductsWithPaginationMutation } from "../apis/productApi";
import { useEffect, useState } from "react";
import { resetSearchCategoryId } from "../redux/slices/searchSlice";

export default function Home() {
  // İleride sepet veya sayaç için kullanılacak Redux state'i:
  const counter = useSelector((state: any) => state.counter?.value || 0); 
  const dispatch = useDispatch();

  const [paginationModel, setPaginationModel] = useState<any>({
    pageNumber: 1,
    pageSize: 8,
    keyword: "",
  });
  const [product, setProduct] = useState<any[]>([]);
  const [totalPages, setTotalPages] = useState(1);
  const [totalItems, setTotalItems] = useState(0);

  // Redux Store'dan arama ve kategori değerlerini okuyoruz:
  const searchValue = useSelector((state: any) => state.searchQuery?.value || "");
  const categoryId = useSelector((state: any) => state.searchQuery?.categoryId || null);

  const [getProductsWithPagination] = useGetProductsWithPaginationMutation();
  
  // 1. Sayfa ilk açıldığında seçili kategori filtresini sıfırlıyoruz:
  useEffect(() => {
    dispatch(resetSearchCategoryId());
  }, [dispatch]);

  // 2. Arama kutusuna yeni bir şey yazıldığında (searchValue değiştikçe) 1. sayfadan arat:
  useEffect(() => {
    setPaginationModel((prev: any) => ({
      ...prev,
      keyword: searchValue,
      pageNumber: 1
    }));
  }, [searchValue]);

  // 3. Menüden bir kategoriye tıklandığında (categoryId değiştikçe) 1. sayfadan filtrele:
  // (İLERİDE: Backend tarafında CategoryId filtresi eklendiğinde aktif olarak çalışacaktır)
  useEffect(() => {
    if (categoryId) {
      setPaginationModel((prev: any) => ({
        ...prev,
        pageNumber: 1,
        categoryId: categoryId
      }));
    }
  }, [categoryId]);

  // 4. Sayfa, arama veya kategori filtresi değiştiğinde ürünleri API'den çek:
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await getProductsWithPagination(paginationModel).unwrap();
        setProduct(response.result?.items || []);
        setTotalPages(Math.ceil((response.result?.totalCount || 0) / paginationModel.pageSize) || 1);
        setTotalItems(response.result?.totalCount || 0);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };

    fetchProducts();
  }, [paginationModel, getProductsWithPagination]);

  // Sayfa değiştirme fonksiyonu
  const handlePageChange = (newPage: number) => {
    setPaginationModel((prev: any) => ({
      ...prev,
      pageNumber: newPage
    }));
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  // 5'li sayfalama butonlarını ve "..." (dots) mantığını oluşturan fonksiyon
  const renderPaginationButtons = () => {
    const buttons = [];
    const maxVisibleButtons = 5;
    let startPage = Math.max(1, paginationModel.pageNumber - Math.floor(maxVisibleButtons / 2));
    let endPage = Math.min(totalPages, startPage + maxVisibleButtons - 1);
    
    if (endPage - startPage + 1 < maxVisibleButtons) {
      startPage = Math.max(1, endPage - maxVisibleButtons + 1);
    }

    // Önceki Sayfa Butonu (<<)
    buttons.push(
      <button 
        key="prev" 
        onClick={() => handlePageChange(Math.max(1, paginationModel.pageNumber - 1))}
        disabled={paginationModel.pageNumber === 1}
        className={styles.paginationButton}
        aria-label="Önceki Sayfa"
      >
        &laquo;
      </button>
    );

    // İlk sayfa (1) ve gerekiyorsa sol üç nokta (...)
    if (startPage > 1) {
      buttons.push(
        <button 
          key="1" 
          onClick={() => handlePageChange(1)}
          className={`${styles.paginationButton} ${1 === paginationModel.pageNumber ? styles.active : ''}`}
        >
          1
        </button>
      );
      if (startPage > 2) {
        buttons.push(<span key="dots1" className={styles.paginationDots}>...</span>);
      }
    }

    // Ortadaki numaralı butonlar
    for (let i = startPage; i <= endPage; i++) {
      buttons.push(
        <button 
          key={i} 
          onClick={() => handlePageChange(i)}
          className={`${styles.paginationButton} ${i === paginationModel.pageNumber ? styles.active : ''}`}
        >
          {i}
        </button>
      );
    }

    // Son sayfa ve gerekiyorsa sağ üç nokta (...)
    if (endPage < totalPages) {
      if (endPage < totalPages - 1) {
        buttons.push(<span key="dots2" className={styles.paginationDots}>...</span>);
      }
      buttons.push(
        <button 
          key={totalPages} 
          onClick={() => handlePageChange(totalPages)}
          className={`${styles.paginationButton} ${totalPages === paginationModel.pageNumber ? styles.active : ''}`}
        >
          {totalPages}
        </button>
      );
    }

    // Sonraki Sayfa Butonu (>>)
    buttons.push(
      <button 
        key="next" 
        onClick={() => handlePageChange(Math.min(totalPages, paginationModel.pageNumber + 1))}
        disabled={paginationModel.pageNumber === totalPages}
        className={styles.paginationButton}
        aria-label="Sonraki Sayfa"
      >
        &raquo;
      </button>
    );

    return buttons;
  };

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Telefonlar</h1>
      <p className={styles.subtitle}>En yeni ve popüler modeller</p>
      
      {/* Ürün Listesi */}
      <div className={styles.productGrid}>
        {product && product.length > 0 ? (
          product.map((phone: any) => (
            <ProductCard key={phone.id} product={phone} />
          ))
        ) : (
          <p style={{ gridColumn: '1 / -1', textAlign: 'center', color: '#6b7280', padding: '2rem 0' }}>
            Aramanıza uygun ürün bulunamadı.
          </p>
        )}
      </div>

      {/* Sayfalama (Pagination) Alanı */}
      {totalPages > 1 && (
        <div className={styles.paginationContainer}>
          <p className={styles.paginationInfo}>
            Toplam {totalItems} ürünün {paginationModel.pageSize * (paginationModel.pageNumber - 1) + 1}-
            {Math.min(paginationModel.pageSize * paginationModel.pageNumber, totalItems)} arası gösteriliyor
          </p>
          <div className={styles.pagination}>
            {renderPaginationButtons()}
          </div>
        </div>
      )}
    </div>
  );
}
