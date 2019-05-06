using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Pdf;
using Android.OS;
using Android.Print;
using Android.Print.Pdf;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.IO;
using CloudMusic.Droid.Services;
using CloudMusic.Services;
using static Android.Print.PrintAttributes;

[assembly: Xamarin.Forms.Dependency(typeof(CrossPrint))]
namespace CloudMusic.Droid.Services
{

    public class CrossPrint : ICrossPrint
    {
        public static Context appcontext;
        private static WebView mWebView;
        public static void Init(Context context)
        {
            appcontext = context;
        }
        public void Print()
        {
            PrintManager printManager = (PrintManager)appcontext.GetSystemService(Context.PrintService);
            printManager.Print("66", new CustomPrintDocumentAdapter(), null);
        }

        public void WebPrint(string Html)
        {
            WebView webView = new WebView(appcontext);
            WebViewClient webViewClient = new CustomWebViewClient();
            webView.SetWebViewClient(webViewClient);
            string htmlDocument = Html;
            webView.LoadDataWithBaseURL(null, htmlDocument, "text/HTML", "UTF-8", null);
            mWebView = webView;
            mWebView.ScrollBarStyle = ScrollbarStyles.InsideOverlay;
            mWebView.Settings.JavaScriptEnabled = true;
            mWebView.Settings.DomStorageEnabled = true;
        }
        static void createWebPrintJob(WebView webView)
        {
            PrintManager printManager = (PrintManager)appcontext.GetSystemService(Context.PrintService);
            String jobName = " Document";
            PrintDocumentAdapter printAdapter = webView.CreatePrintDocumentAdapter(jobName);
            PrintJob printJob = printManager.Print(jobName, printAdapter, new PrintAttributes.Builder().Build());
            printAdapter.OnStart();
            //printJobs.add(printJob);
        }
        class CustomWebViewClient : WebViewClient
        {
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
                createWebPrintJob(view);
                mWebView = null;
            }
        }
        class CustomPrintDocumentAdapter : PrintDocumentAdapter
        {
            PrintedPdfDocument mPdfDocument;
            public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
            {
                mPdfDocument = new PrintedPdfDocument(CrossPrint.appcontext, newAttributes);

                if (cancellationSignal.IsCanceled)
                {
                    callback.OnLayoutCancelled();
                    return;
                }
                int pages = computePageCount(newAttributes);

                if (pages > 0)
                {
                    PrintDocumentInfo info = new PrintDocumentInfo
                    .Builder("pdf.pdf")
                    .SetContentType(PrintContentType.Document)
                    .SetPageCount(pages).Build();
                    callback.OnLayoutFinished(info, true);
                }
                else
                {
                    callback.OnLayoutFailed("Page count calculation failed.");
                }
            }
            private Boolean pageInRange(PageRange[] pageRanges, int page)
            {
                for (int i = 0; i < pageRanges.Length; i++)
                {
                    if ((page >= pageRanges[i].Start) &&
                            (page <= pageRanges[i].End))
                        return true;
                }
                return false;
            }

            public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
            {
                int totalPages = 1;
                for (int i = 0; i < totalPages; i++)
                {
                    if (pageInRange(pages, i))
                    {
                        PdfDocument.Page[] writtenPagesArray;
                        PdfDocument.Page page = mPdfDocument.StartPage(i);
                        if (cancellationSignal.IsCanceled)
                        {
                            callback.OnWriteCancelled();
                            mPdfDocument.Close();
                            mPdfDocument = null;
                            return;
                        }
                        drawPage(page);
                        mPdfDocument.FinishPage(page);
                    }
                }
                try
                {
                    FileOutputStream fileOutputStream = new FileOutputStream(destination.FileDescriptor);
                    MemoryStream stream = new MemoryStream();
                    mPdfDocument.WriteTo(stream);
                    byte[] bytes = stream.ToArray();
                    stream.Close();
                    fileOutputStream.Write(bytes);
                }
                catch (Java.IO.IOException e)
                {
                    callback.OnWriteFailed(e.ToString());
                    return;
                }
                finally
                {
                    mPdfDocument.Close();
                    mPdfDocument = null;
                }
                callback.OnWriteFinished(pages);
            }
            private int computePageCount(PrintAttributes printAttributes)
            {
                int itemsPerPage = 4;

                MediaSize pageSize = printAttributes.GetMediaSize();
                if (!pageSize.IsPortrait)
                {
                    itemsPerPage = 6;
                }
                int printItemCount = 6;

                return (int)Math.Ceiling((double)(printItemCount / itemsPerPage));
            }
            private void drawPage(PdfDocument.Page page)
            {
                Canvas canvas = page.Canvas;
                int titleBaseLine = 72;
                int leftMargin = 54;

                Paint paint = new Paint();
                paint.Color = Color.Black;
                paint.TextSize = 36;
                canvas.DrawText("Test Title", leftMargin, titleBaseLine, paint);

                paint.TextSize = 11;
                canvas.DrawText("Test paragraph", leftMargin, titleBaseLine + 25, paint);

                paint.Color = Color.Blue;
                canvas.DrawRect(100, 100, 172, 172, paint);
            }
        }

    }
}


