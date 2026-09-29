"use client";
import { useState, useEffect } from "react";
import { useGetProductsWithPaginationMutation } from "../apis/productApi";
import ProductCard from "./components/ProductCard";
import { FiChevronLeft, FiChevronRight } from "react-icons/fi";

export default function Home() {
  const [paginationModel, setPaginationModel] = useState({
    pageNumber: 1,
    pageSize: 8,
    keyword: '',
  });

  const [product, setProduct] = useState([]);
  const [totalCount, setTotalCount] = useState(0);

  // 1. Hook'tan tetikleyici fonksiyonu ve istek durumlarını çekiyoruz:
  const [getProductsWithPagination, { isLoading, isError }] = useGetProductsWithPaginationMutation();

  // 2. Sayfa açıldığında veya paginationModel değiştiğinde isteği atıyoruz:
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await getProductsWithPagination(paginationModel).unwrap();
        console.log("Pagination Response:", response);
        setProduct(response.result?.items || []);
        setTotalCount(response.result?.totalCount || 0);
      } catch (error) {
        console.error('Error occurred when fetching products', error);
      }
    };
    fetchProducts();
  }, [paginationModel, getProductsWithPagination]);

  const totalPages = Math.ceil(totalCount / paginationModel.pageSize) || 1;

  const handlePageChange = (newPage: number) => {
    if (newPage < 1 || newPage > totalPages || newPage === paginationModel.pageNumber) return;
    setPaginationModel((prev) => ({
      ...prev,
      pageNumber: newPage,
    }));
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  return (
    <main className="container mx-auto p-4 min-h-screen flex flex-col justify-between">
      {/* Ürün Listesi */}
      <div>
        {isLoading ? (
          <div className="p-16 text-center text-lg text-gray-600">Ürünler Yükleniyor...</div>
        ) : isError ? (
          <div className="p-16 text-center text-red-500 font-semibold">Ürünler yüklenirken bir hata oluştu!</div>
        ) : product && product.length > 0 ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {product.map((item: any) => (
              <ProductCard key={item.id} product={item} />
            ))}
          </div>
        ) : (
          <div className="p-16 text-center text-gray-500">Kayıtlı ürün bulunamadı.</div>
        )}
      </div>

      {/* Pagination (Sayfalama) Kontrolleri */}
      {totalPages > 1 && (
        <div className="my-10 flex flex-col sm:flex-row items-center justify-between gap-4 border-t border-gray-200 pt-6">
          <div className="text-sm text-gray-500">
            Toplam <span className="font-semibold text-gray-800">{totalCount}</span> üründen{' '}
            <span className="font-semibold text-gray-800">
              {(paginationModel.pageNumber - 1) * paginationModel.pageSize + 1}
            </span>
            -
            <span className="font-semibold text-gray-800">
              {Math.min(paginationModel.pageNumber * paginationModel.pageSize, totalCount)}
            </span>{' '}
            arası gösteriliyor
          </div>

          <div className="flex items-center gap-2">
            {/* Önceki Sayfa Butonu */}
            <button
              onClick={() => handlePageChange(paginationModel.pageNumber - 1)}
              disabled={paginationModel.pageNumber === 1 || isLoading}
              className={`flex items-center gap-1 px-3 py-2 rounded-lg border text-sm font-medium transition-colors ${
                paginationModel.pageNumber === 1 || isLoading
                  ? 'border-gray-200 text-gray-300 cursor-not-allowed bg-gray-50'
                  : 'border-gray-300 text-gray-700 hover:bg-gray-100 cursor-pointer bg-white'
              }`}
            >
              <FiChevronLeft size={16} />
              Önceki
            </button>

            {/* Sayfa Numaraları */}
            <div className="flex items-center gap-1">
              {Array.from({ length: totalPages }, (_, i) => i + 1).map((pageNum) => {
                const isActive = pageNum === paginationModel.pageNumber;
                return (
                  <button
                    key={pageNum}
                    onClick={() => handlePageChange(pageNum)}
                    disabled={isLoading}
                    className={`min-w-9 h-9 px-3 rounded-lg text-sm font-medium transition-all ${
                      isActive
                        ? 'bg-blue-600 text-white font-bold shadow-sm'
                        : 'bg-white border border-gray-200 text-gray-700 hover:bg-gray-100 hover:border-gray-300'
                    }`}
                  >
                    {pageNum}
                  </button>
                );
              })}
            </div>

            {/* Sonraki Sayfa Butonu */}
            <button
              onClick={() => handlePageChange(paginationModel.pageNumber + 1)}
              disabled={paginationModel.pageNumber === totalPages || isLoading}
              className={`flex items-center gap-1 px-3 py-2 rounded-lg border text-sm font-medium transition-colors ${
                paginationModel.pageNumber === totalPages || isLoading
                  ? 'border-gray-200 text-gray-300 cursor-not-allowed bg-gray-50'
                  : 'border-gray-300 text-gray-700 hover:bg-gray-100 cursor-pointer bg-white'
              }`}
            >
              Sonraki
              <FiChevronRight size={16} />
            </button>
          </div>
        </div>
      )}
    </main>
  );
}
