using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FileTypeDetectiveDotnetCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestsDotnetCore
{
    [TestClass]
    public class DetectiveTest
    {
        String _basePath;
        FileInfo _emptyFile;
        FileInfo _pdfFile;
        FileInfo _wordFile;
        FileInfo _excelFile;
        FileInfo _jpegFile;
        FileInfo _zipFile;
        FileInfo _rarFile;
        FileInfo _rtfFile;
        FileInfo _noTypeFile;
        FileInfo _pngFile;
        FileInfo _pptFile;
        FileInfo _gifFile;
        FileInfo _exeFile;
        FileInfo _msiFile;
        private const string FilesDir = "Files";

        [TestInitialize]
        public void SetUp()
        {
            _basePath = AppContext.BaseDirectory;
            _basePath = _basePath.Replace("file:\\", "");
            // add the Files directory 
            _basePath = Path.Combine(_basePath, FilesDir);

            _noTypeFile = new FileInfo(Path.Combine(_basePath, "notypeFile.xxx"));
            _emptyFile = new FileInfo(Path.Combine(_basePath, "EmptyFile.txt"));
            _pdfFile = new FileInfo(Path.Combine(_basePath, "pdfFile.pdf"));
            _wordFile = new FileInfo(Path.Combine(_basePath, "WordFile.doc"));
            _excelFile = new FileInfo(Path.Combine(_basePath, "excelFile.xls"));
            _jpegFile = new FileInfo(Path.Combine(_basePath, "jpegFile.jpg"));
            _zipFile = new FileInfo(Path.Combine(_basePath, "zipFile.zip"));
            _rarFile = new FileInfo(Path.Combine(_basePath, "rarFile.rar"));
            _rtfFile = new FileInfo(Path.Combine(_basePath, "rtfFile.rtf"));
            _pngFile = new FileInfo(Path.Combine(_basePath, "pngFile.png"));
            _pptFile = new FileInfo(Path.Combine(_basePath, "pptFile.ppt"));
            _gifFile = new FileInfo(Path.Combine(_basePath, "gif.gif"));
            _exeFile = new FileInfo(Path.Combine(_basePath, "cacheCopy.exe"));
            _msiFile = new FileInfo(Path.Combine(_basePath, "cacheCopySetup.msi"));
        }

        [TestMethod]
        public void EmptyFileTest()
        {
            FileType empty = _emptyFile.GetFileType();
            Assert.IsNull(empty);

            empty = _emptyFile.GetFileType();
            Assert.IsNull(empty);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NoFileTest()
        {
            FileInfo noFile = new FileInfo("someLongNmame_of_file_that_does_notExists");
            if (noFile.Exists)
            {
                Assert.Fail("Exists file that should not exist");
            }

            noFile.GetFileType();
        }

        [TestMethod]
        public void IsPdfTest()
        {
            Assert.IsTrue(_pdfFile.IsPdf());
            Assert.IsFalse(_noTypeFile.IsPdf());
        }

        [TestMethod]
        public void IsWordTest()
        {
            Assert.IsTrue(_wordFile.IsWord());
            Assert.IsFalse(_noTypeFile.IsWord());
        }

        [TestMethod]
        public void IsExcelTest()
        {
            Assert.IsTrue(_excelFile.IsExcel());
            Assert.IsFalse(_noTypeFile.IsExcel());
        }

        [TestMethod]
        public void IsJpegTest()
        {
            Assert.IsTrue(_jpegFile.IsJpeg());
            Assert.IsFalse(_noTypeFile.IsJpeg());
        }

        [TestMethod]
        public void IsZipTest()
        {
            Assert.IsTrue(_zipFile.IsZip());
            Assert.IsFalse(_noTypeFile.IsZip());
        }

        [TestMethod]
        public void IsRarTest()
        {
            Assert.IsTrue(_rarFile.IsRar());
            Assert.IsFalse(_noTypeFile.IsRar());
        }


        [TestMethod]
        public void IsRtfTest()
        {
            Assert.IsTrue(_rtfFile.IsRtf());
            Assert.IsFalse(_noTypeFile.IsRtf());

        }

        [TestMethod]
        public void IsFileOfTypesCsvTest()
        {
            Assert.IsTrue(_jpegFile.isFileOfTypes("JPG,RAR,DOC,XLS"));
            Assert.IsFalse(_jpegFile.isFileOfTypes(""));
            Assert.IsFalse(_jpegFile.isFileOfTypes("RAR"));
            Assert.IsTrue(_jpegFile.isFileOfTypes("JPG"));
        }

        [TestMethod]
        public void IsFileOfTypesList()
        {
            Assert.IsTrue(_jpegFile.isFileOfTypes(new List<FileType> { Detective.JPEG }));
            Assert.IsFalse(_jpegFile.isFileOfTypes(new List<FileType> { Detective.RAR }));
            Assert.IsFalse(_jpegFile.isFileOfTypes(new List<FileType>()));
            Assert.IsFalse(_jpegFile.isFileOfTypes(new List<FileType> { Detective.RTF, Detective.PDF, Detective.EXCEL }));
            Assert.IsTrue(_jpegFile.isFileOfTypes(new List<FileType> { Detective.JPEG, Detective.RTF, Detective.PDF, Detective.EXCEL }));

        }

        [TestMethod]
        public void IsPngTest()
        {
            Assert.IsTrue(_pngFile.IsPng());
            Assert.IsFalse(_pngFile.IsPdf());
            Assert.IsFalse(_pngFile.IsJpeg());
        }

        [TestMethod]
        public void IsPptTest()
        {
            Assert.IsTrue(_pptFile.IsPpt());
            Assert.IsFalse(_pptFile.IsJpeg());
            Assert.IsFalse(_pptFile.IsPng());
        }

        [TestMethod]
        public void IsGifTest()
        {
            Assert.IsFalse(_gifFile.IsPdf());
            Assert.IsTrue(_gifFile.IsGif());
        }

        [TestMethod]
        public void IsExeTest()
        {
            Assert.IsFalse(_exeFile.IsJpeg());
            Assert.IsTrue(_exeFile.IsExe());
        }

        [TestMethod]
        public void IsMsiTest()
        {
            Assert.IsFalse(_msiFile.IsExe());
            Assert.IsTrue(_msiFile.IsMsi());
        }
    }
}